namespace RabbitMQEventBus.Subscription;

public class Subscription
{
    public Type EventType { get; init; }
    public HashSet<Type> Handlers { get; }

    public Subscription(Type Tevent, Type Thandler)
    {
        EventType = Tevent;
        Handlers = new() { Thandler };
    }

    public void Add(Type Thandler) => Handlers.Add(Thandler);
}