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
                        }
                        catch (Exception e)
                        {
                            logger.LogError($"Could not create a connection to RabbitMQ, error details => {e.Message}");
                            return false;
                        }
                    }
                }
            }

            logger.LogInformation("A connection to RabbitMQ has been established!");
            return true;
        }

        public IModel CreateChannel()
        {
            if (!IsConnected) return default;

            return connection.CreateModel();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                try
                {
                    connection.Dispose();
                    disposed = true;
                }
                catch (Exception e)
                {
                    logger.LogError($"Could not close the connection to RabbitMQ, error details => {e.Message}");
                }
            }
        }
    }
}