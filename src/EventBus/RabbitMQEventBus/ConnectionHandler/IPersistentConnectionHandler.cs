using RabbitMQ.Client;
using System;

namespace RabbitMQEventBus.ConnectionHandler
{
    public interface IPersistentConnectionHandler : IDisposable
    {
        /// <summary>
        /// Returns true if a connection has been created otherwise tries to create a new one
        /// </summary>
        public bool CanEstablishConnection();

        public IModel CreateChannel();
    }
}