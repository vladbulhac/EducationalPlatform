using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Repository_Results
{
    /// <summary>
    /// Contains data needed by the handler to publish an <see cref="IntegrationEvent"/>
    /// </summary>
    public record CommandRepositoryResult
    {
        public ICollection<Guid> AdminsToNotify { get; init; }

        public CommandRepositoryResult(ICollection<Guid> adminsToNotify) => AdminsToNotify = adminsToNotify;
    }
}