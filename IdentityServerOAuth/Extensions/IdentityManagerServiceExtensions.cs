using IdentityManager;
using IdentityManager.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using IdentityServerOAuth.Entities;

namespace IdentityServerOAuth.Extensions
{
    public static class IdentityManagerServiceExtensions
    {
        public static IdentityManagerServiceFactory Configure(this IdentityManagerServiceFactory factory,
            string connectionString)
        {

            factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<RoleStore>());
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<RoleManager>());
            var clientstore = new ClientStore(new ClientConfigurationDbContext(connectionString));
            factory.Register(new Registration<IClientStore>(clientstore));

            factory.IdentityManagerService = new Registration<IIdentityManagerService, IdentityManagerService>();

            return factory;
        }
    }
}