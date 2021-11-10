using Microsoft.EntityFrameworkCore;

namespace DatabaseTasks.ApplyMigrations_Task;

public class ApplyEFMigrationsToSQLServerDatabase : IHostedService, IAsyncDisposable
{
    /// <summary>
    /// Schedules <see cref="ApplyPendingMigrationsAsync(object)">ApplyPendingMigrationsAsync method</see>
    /// to retry applying the pending Migrations for each <see cref="DbContext"/> in  <see cref="deadLetterQueue">dead-letter queue</see>
    /// </summary>
    private Timer timer;

    /// <summary>
    /// The maximum number of times the <see cref="deadLetterQueue">dead-letter queue</see> is traversed
    /// </summary>
    private int retries;

    private const int secondsRequiredForApplyingMigrations = 15;

    /// <summary>
    /// Contains <see cref="DbContext">DbContexts</see> for which Migrations are attempted for the first time
    /// </summary>
    private readonly Queue<DbContext> contexts;

    /// <summary>
    /// Contains <see cref="DbContext">DbContexts</see> for which Migrations could not be applied
    /// </summary>
    private readonly Queue<DbContext> deadLetterQueue;

    private readonly ILogger<ApplyEFMigrationsToSQLServerDatabase> logger;

    public ApplyEFMigrationsToSQLServerDatabase(IEnumerable<DbContext> contexts, ILogger<ApplyEFMigrationsToSQLServerDatabase> logger, int retries = 3)
    {
        if (contexts is null) throw new ArgumentNullException(nameof(contexts));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        deadLetterQueue = new();
        this.contexts = new(contexts);

        this.retries = retries;
    }

    public async Task StartAsync(CancellationToken cancellationToken) => await ApplyMigrationsForTheFirstTimeAsync();

    private async Task ApplyMigrationsForTheFirstTimeAsync()
    {
        if (contexts.Count == 0)
            return;

        await foreach (var _ in ApplyMigrationsFromQueueAsync(contexts))
        {
            logger.LogInformation("Applying Entity Framework Migrations to database...");
        }

        if (deadLetterQueue.Count > 0)
            timer = new Timer(ApplyPendingMigrationsAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(secondsRequiredForApplyingMigrations * deadLetterQueue.Count));
    }

    private async void ApplyPendingMigrationsAsync(object state)
    {
        if (deadLetterQueue.Count == 0 || retries <= 0)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            return;
        }

        int pendingMigrations = deadLetterQueue.Count;

        await foreach (var retriedMigrationNumber in ApplyMigrationsFromQueueAsync(deadLetterQueue))
        {
            if (retriedMigrationNumber >= pendingMigrations)
                break;

            logger.LogInformation("Applying pending Entity Framework Migrations to database...");
        }

        if (deadLetterQueue.Count > 0)
        {
            retries--;

            if (retries > 0)
                logger.LogInformation("There are {0} pending Migrations, {1} retries left!", deadLetterQueue.Count, retries);
            else
            {
                logger.LogWarning("Not all Migrations could be applied! Applying Migrations failed on the following databases: {0}",
                                  deadLetterQueue.Select(context => new
                                  {
                                      context.Database.GetDbConnection().DataSource,
                                      context.Database.GetDbConnection().Database
                                  }).ToList());
            }
        }
    }

    /// <returns>The index in the <paramref name="queueOfContexts"/> of each <see cref="DbContext"/> for which the Migrations are tried to be applied</returns>
    private async IAsyncEnumerable<int> ApplyMigrationsFromQueueAsync(Queue<DbContext> queueOfContexts)
    {
        int attemptedMigrationsCounter = 0;

        while (queueOfContexts.Count > 0)
        {
            yield return attemptedMigrationsCounter++;
            var context = queueOfContexts.Dequeue();

            try
            {
                await context.Database.MigrateAsync();

                logger.LogInformation("Successfully applied Migrations to the database: {0} - {1} !",
                                      context.Database.GetDbConnection().DataSource,
                                      context.Database.GetDbConnection().Database);

                await context.DisposeAsync();
            }
            catch (Exception e)
            {
                logger.LogInformation("Could not apply Migrations to the database: {0} - {1} right now due to => {2}... retrying again after a minimum delay of {3} seconds!",
                                       context.Database.GetDbConnection().DataSource,
                                       context.Database.GetDbConnection().Database,
                                       e.Message,
                                       secondsRequiredForApplyingMigrations * (queueOfContexts.Count + deadLetterQueue.Count));

                deadLetterQueue.Enqueue(context);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (timer is not null)
            timer.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        if (contexts is not null && contexts.Count > 0)
            foreach (var context in contexts)
                await context.DisposeAsync();

        if (deadLetterQueue is not null && deadLetterQueue.Count > 0)
            foreach (var context in deadLetterQueue)
                await context.DisposeAsync();

        if (timer is not null)
            await timer.DisposeAsync();
    }
}