using RabbitMQ.Client;
using System;

namespace RabbitMQEventBus.ConnectionHandler
{
    /// <summary>
    /// <para> Keeps a connection open for the entire runtime of the application </para>
    /// <para> Handles the connection status, creation and closure </para>
    /// </summary>
    public interface IPersistentConnectionHandler : IDisposable
    {
        /// <summary>
        /// Returns true if a connection has been created otherwise tries to create a new one
        /// </summary>
        public bool CanEstablishConnection();

        public IModel GetTransientChannel();

        public IModel GetPersistentChannel();
    }
}