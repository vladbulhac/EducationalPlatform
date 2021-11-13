using EducationalInstitution.Domain.Models;

namespace EducationalInstitution.Domain.Building_Blocks;

/// <summary>
/// Represents an object that is defined by its identity and not by its attributes.
/// </summary>
public abstract class Entity<TKey>
{
    public TKey Id { get; init; }

    public Access Access { get; protected set; }

    public virtual void ScheduleForDeletion(double daysUntilDeletion = 30)
    {
        if (daysUntilDeletion <= 0) throw new ArgumentOutOfRangeException(nameof(daysUntilDeletion), "Value must be greater than 0!");

        DateTime currentDateAndTime = DateTime.UtcNow.Date;

        Access = new()
        {
            IsDisabled = true,
            DateForPermanentDeletion = currentDateAndTime.AddDays(daysUntilDeletion)
                                                         .ToUniversalTime()
        };
    }

    public void ClearDeletionDate() => Access = new() { IsDisabled = false, DateForPermanentDeletion = null };

    public DateTime? GetDeletionDate()
    {
        if (!Access.IsDisabled) return null;

        return Access.DateForPermanentDeletion;
    }
}

/// <summary>
/// <para>Id type is <see cref="Guid"/></para>
/// <inheritdoc cref="Entity{TKey}"/>
/// </summary>
public abstract class GuidEntity : Entity<Guid>
{
    protected GuidEntity()
    {
    }

    protected GuidEntity(Guid? id)
    {
        if (id.HasValue && id == Guid.Empty) throw new ArgumentException($"{nameof(id)} doesn't have a valid value!");

        Id = id ?? Guid.NewGuid();
        Access = new();
    }
}

/// <summary>
/// <para>Id type is <see cref="string"/></para>
/// <inheritdoc cref="Entity{TKey}"/>
/// </summary>
public abstract class StringEntity : Entity<string>
{
    protected StringEntity()
    {
    }

    protected StringEntity(string id)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Access = new();
    }
}