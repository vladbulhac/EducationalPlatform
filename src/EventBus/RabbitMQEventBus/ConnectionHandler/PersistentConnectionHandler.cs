using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace RabbitMQEventBus.ConnectionHandler
{
    /// <inheritdoc cref="IPersistentConnectionHandler"/>
    public class PersistentConnectionHandler : IPersistentConnectionHandler
    {
        private bool disposed;
        private readonly object padlock = new();

        private IConnection connection;
        private readonly IConnectionFactory connectionFactory;
        private readonly ILogger<PersistentConnectionHandler> logger;

        private IModel persistentChannel;

        private bool IsConnected { get => connection != null ? connection.IsOpen : false; }

        public PersistentConnectionHandler(ILogger<PersistentConnectionHandler> logger, IConnectionFactory connectionFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool CanEstablishConnection()
        {
            if (!IsConnected)
            {
                lock (padlock)
                {
                    if (!IsConnected)
                    {
                        try
                        {
                            connection = connectionFactory.CreateConnection();
                            logger.LogInformation("A connection to RabbitMQ has been established!");
                        }
                        catch (Exception e)
                        {
                            logger.LogError($"Could not create a connection to RabbitMQ, error details => {e.Message}");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public IModel GetTransientChannel()
        {
            if (!IsConnected) return default;

            return connection.CreateModel();
        }

        public IModel GetPersistentChannel()
        {
            if (!IsConnected) return default;

            if (persistentChannel == default || persistentChannel.IsClosed)
            {
                persistentChannel = connection.CreateModel();
                logger.LogDebug("A persistent channel for receiving events was created successfully!");
            }

            return persistentChannel;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    persistentChannel?.Dispose();
                    connection?.Dispose();
                }
            }

            disposed = true;
        }
    }
}