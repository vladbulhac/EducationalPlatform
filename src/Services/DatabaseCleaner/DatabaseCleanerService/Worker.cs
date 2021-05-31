using Dapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseCleanerService
{
    /// <summary>
    /// Periodically removes entities that are scheduled for deletion from the provided database
    /// </summary>
    public class Worker : IHostedService, IAsyncDisposable
    {
        private readonly ILogger<Worker> logger;
        private readonly string dbConnectionString;
        private readonly int retryInHours;
        private Timer timer;

        public Worker(string connectionString, int retryInHours, ILogger<Worker> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.retryInHours = retryInHours <= 0 ? throw new Exception($"{nameof(retryInHours)} must be a positive number greater than 0!") : retryInHours;

            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            dbConnectionString = connectionString;
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            timer = new Timer(ExecuteAsync, null, TimeSpan.Zero, TimeSpan.FromHours(retryInHours));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void ExecuteAsync(object state)
        {
            await using (var connection = new SqlConnection(dbConnectionString))
            {
                await connection.OpenAsync();

                logger.LogDebug($"{this.GetType().Namespace} established connection to database with connection string: {dbConnectionString}... proceeding with deleting all entitites that are scheduled today for deletion!");

                var tables = await GetAllTablesFromDatabase(connection);

                await DeleteScheduledEntitiesForToday(connection, tables);

                logger.LogDebug("Database cleaned up... closing connection now");
            }
        }

        private async Task<IEnumerable<string>> GetAllTablesFromDatabase(SqlConnection connection)
        {
            return await connection.QueryAsync<string>(@"SELECT TABLE_NAME
                                                                FROM INFORMATION_SCHEMA.TABLES
                                                                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '__EFMigrationsHistory'");
        }

        private async Task DeleteScheduledEntitiesForToday(SqlConnection connection, IEnumerable<string> tables)
        {
            var todaysDate = DateTime.UtcNow;
            foreach (var tablename in tables)
            {
                var statement = string.Format(@"DELETE FROM {0}
                                                WHERE IsDisabled=1
                                                      AND DAY(DateForPermanentDeletion)={1}
                                                      AND MONTH(DateForPermanentDeletion)={2}
                                                      AND YEAR(DateForPermanentDeletion)={3}",
                                                  tablename,
                                                  todaysDate.Day,
                                                  todaysDate.Month,
                                                  todaysDate.Year);

                await connection.QueryAsync(statement);
            }
        }

        public async ValueTask DisposeAsync() => await timer.DisposeAsync();
    }
}