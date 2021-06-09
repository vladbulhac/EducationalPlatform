using System;
using System.Collections.Generic;

namespace RabbitMQEventBus.Subscription
{
    /// <inheritdoc cref="ISubscriptionManager"/>
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly IDictionary<string, HashSet<Type>> eventNameToHandlersMap;
        private readonly IDictionary<string, Type> eventNameToTypeMap;

        public SubscriptionManager()
        {
            eventNameToHandlersMap = new Dictionary<string, HashSet<Type>>();
            eventNameToTypeMap = new Dictionary<string, Type>();
        }

        public void SaveSubscription(Type @event, Type handler)
        {
            var eventName = @event.Name;
            if (!eventNameToHandlersMap.ContainsKey(eventName))
            {
                eventNameToHandlersMap.Add(eventName, new HashSet<Type>() { handler });
                eventNameToTypeMap.Add(eventName, @event);
            }
            else
                eventNameToHandlersMap[eventName].Add(handler);
        }

        public ICollection<Type> GetHandlersOfEvent(string eventName)
        {
            if (!eventNameToHandlersMap.ContainsKey(eventName))
                return default;

            return eventNameToHandlersMap[eventName];
        }

        public Type GetTypeOfEvent(string eventName)
        {
            if (!eventNameToTypeMap.ContainsKey(eventName))
                return default;

            return eventNameToTypeMap[eventName];
        }

        public bool HasSubscription(string eventName) => eventNameToHandlersMap.ContainsKey(eventName);
    }
}