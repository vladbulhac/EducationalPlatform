using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace RabbitMQEventBus.Subscription
{
    /// <summary>
    /// Keeps track of which <see cref="IIntegrationEventHandler{TIntegrationEvent}"/> handles which <see cref="IntegrationEvent"/>
    /// </summary>
    public interface ISubscriptionManager
    {
        public void SaveSubscription(Type eventType, Type handler);

        public ICollection<Type> GetHandlersOfEvent(string eventType);

        public bool HasSubscription(string eventType);

        public Type GetTypeOfEvent(string eventName);
    }
}