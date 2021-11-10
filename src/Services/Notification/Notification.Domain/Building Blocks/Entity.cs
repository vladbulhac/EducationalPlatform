namespace Notification.Domain.Building_Blocks;

/// <summary>
/// Represents an object that is defined by its identity and not by its attributes.
/// </summary>
public abstract class Entity
{
    public string Id { get; init; }

    protected Entity()
    {
    }

    protected Entity(string id) => Id = id ?? Guid.NewGuid().ToString();
}