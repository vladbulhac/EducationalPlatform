using OpenIddict.Abstractions;

namespace Identity.API.Configuration.Client_Scopes;

public static class DefinedScopes
{
    public class EducationalInstitutionScopes : IScope
    {
        public const string All = "client.educational_institution.all";
        public const string UpdateDetails = "client.educational_institution.update_details";
        public const string ChangeAdministrators = "client.educational_institution.change_administrators";
        public const string Delete = "client.educational_institution.delete";

        public async Task RegisterScopes(IOpenIddictScopeManager scopeManager)
        {
            await CreateAllScope(scopeManager);
            await CreateDeleteScope(scopeManager);
            await CreateUpdateDetailsScope(scopeManager);
            await CreateChangeAdministratorsScope(scopeManager);
        }

        private async Task CreateAllScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(All) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = All,
                Resources =
                    {
                        "aggregator",
                        "educational_institution_api"
                    }
            });
        }

        private async Task CreateDeleteScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(Delete) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = Delete,
                Resources =
                    {
                        "aggregator",
                        "educational_institution_api"
                    }
            });
        }

        private async Task CreateUpdateDetailsScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(UpdateDetails) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = UpdateDetails,
                Resources =
                    {
                        "aggregator",
                        "educational_institution_api"
                    }
            });
        }

        private async Task CreateChangeAdministratorsScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(ChangeAdministrators) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = ChangeAdministrators,
                Resources =
                    {
                        "aggregator",
                        "educational_institution_api"
                    }
            });
        }

        public IEnumerable<string> GetAllScopes()
        {
            yield return All;
            yield return ChangeAdministrators;
            yield return Delete;
            yield return UpdateDetails;
        }
    }

    public class NotificationScopes : IScope
    {
        public const string All = "client.notification.all";
        public const string Receive = "client.notification.receive";
        public const string Delete = "client.notification.delete";

        public async Task RegisterScopes(IOpenIddictScopeManager scopeManager)
        {
            await CreateAllScope(scopeManager);
            await CreateReceiveScope(scopeManager);
            await CreateDeleteScope(scopeManager);
        }

        private async Task CreateAllScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(All) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = All,
                Resources =
                {
                        "aggregator",
                        "notification_api"
                }
            });
        }

        private async Task CreateReceiveScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(Receive) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = Receive,
                Resources =
                {
                        "aggregator",
                        "notification_api"
                }
            });
        }

        private async Task CreateDeleteScope(IOpenIddictScopeManager scopeManager)
        {
            if (await scopeManager.FindByNameAsync(Delete) is not null) return;

            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = Delete,
                Resources =
                {
                        "aggregator",
                        "notification_api"
                }
            });
        }

        public IEnumerable<string> GetAllScopes()
        {
            yield return All;
            yield return Receive;
            yield return Delete;
        }
    }
}