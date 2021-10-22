using System;
using System.Collections.Generic;

namespace RabbitMQEventBus.Subscription
{
    /// <inheritdoc cref="ISubscriptionManager"/>
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly Dictionary<string, Subscription> eventNameToSubscriptionMap;

        public SubscriptionManager() => eventNameToSubscriptionMap = new();

        public void SaveSubscription(Type @event, Type handler)
        {
            var eventName = @event.Name;

            switch (HasSubscription(eventName))
            {
                case true:
                    eventNameToSubscriptionMap[eventName].Add(handler);
                    break;

                default:
                    eventNameToSubscriptionMap.Add(eventName, new Subscription(@event, handler));
                    break;
            }
        }

        public Subscription GetSubscriptionDetailsOfEvent(string eventName) => HasSubscription(eventName) ? eventNameToSubscriptionMap[eventName] : default;

        private bool HasSubscription(string eventName) => eventNameToSubscriptionMap.ContainsKey(eventName);

        public void ClearResources() => eventNameToSubscriptionMap.Clear();
    }
}