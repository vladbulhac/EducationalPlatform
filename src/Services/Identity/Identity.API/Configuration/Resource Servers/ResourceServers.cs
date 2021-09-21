using OpenIddict.Abstractions;
using System.Collections.Generic;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API.Configuration.Resource_Servers
{
    public class ResourceServers : IResourceServers<OpenIddictApplicationDescriptor>
    {
        private OpenIddictApplicationDescriptor aggregator;
        private OpenIddictApplicationDescriptor notification_api;
        private OpenIddictApplicationDescriptor educational_institution_api;

        public ResourceServers()
        {
            CreateAggregator();
            CreateEducationalInstitutionAPI();
            CreateNotificationAPI();
        }

        public IEnumerable<OpenIddictApplicationDescriptor> GetResourceServers()
        {
            yield return aggregator;
            yield return notification_api;
            yield return educational_institution_api;
        }

        private void CreateAggregator()
        {
            aggregator = new OpenIddictApplicationDescriptor
            {
                ClientId = nameof(aggregator),
                ClientSecret = "466r36470r",
                Permissions =
                {
                   Permissions.Endpoints.Introspection
                }
            };
        }

        private void CreateEducationalInstitutionAPI()
        {
            educational_institution_api = new OpenIddictApplicationDescriptor
            {
                ClientId = nameof(educational_institution_api),
                ClientSecret = "3du-1n5717u10n",
                Permissions =
                {
                   Permissions.Endpoints.Introspection
                }
            };
        }

        private void CreateNotificationAPI()
        {
            notification_api = new OpenIddictApplicationDescriptor
            {
                ClientId = nameof(notification_api),
                ClientSecret = "n071f1c4710n",
                Permissions =
                {
                   Permissions.Endpoints.Introspection
                }
            };
        }
    }
}