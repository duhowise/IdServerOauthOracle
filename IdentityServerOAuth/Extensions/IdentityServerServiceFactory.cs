using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.EntityFramework;
using IdentityServerOAuth.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Extensions
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory,
            string connectionString)
        {

           var context = new Context(connectionString);
            var serviceOptions = new EntityFrameworkServiceOptions {ConnectionString = connectionString};
            SetupClients(InMemoryManager.GetClients(), serviceOptions);
            SetupScopes(InMemoryManager.GetScopes(), serviceOptions);
            SetupUsers(InMemoryManager.GetUsers(), serviceOptions);
            factory.RegisterOperationalServices(serviceOptions);
            factory.RegisterConfigurationServices(serviceOptions);
            new TokenCleanup(serviceOptions, 1).Start();
            factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<UserManager>());
            var userService =
                new AspNetIdentityUserService<IdentityUser, string>(new UserManager(new UserStore(context)));
            factory.UserService = new Registration<IUserService>(userService);
            return factory;

        }

        private static void SetupUsers(List<InMemoryUser> getUsers, EntityFrameworkServiceOptions serviceOptions)
        {
            try
            {
                using (var context = new IdentityDbContext(serviceOptions.ConnectionString))
                {
                    if (context.Users.Any()) return;

                    foreach (var user in getUsers)
                    {
                        var identityuser = new IdentityUser
                        {
                            UserName = user.Username,
                            PasswordHash = user.Password.Sha256(),

                        };
                        identityuser.Claims.Add(new IdentityUserClaim
                        {
                            ClaimType = "Subject",
                            ClaimValue = user.Subject,
                            UserId = identityuser.Id


                        });
                        identityuser.Claims.Add(
                            new IdentityUserClaim
                            {
                                ClaimType = "Name",
                                ClaimValue = "Duhp Dance",
                                UserId = identityuser.Id
                            });

                        context.Users.Add(identityuser);

                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }
        public static void SetupScopes(IEnumerable<Scope> scopes,
            EntityFrameworkServiceOptions options)
        {
            using (var context =
                new ScopeConfigurationDbContext(options.ConnectionString,
                    options.Schema))
            {
                if (context.Scopes.Any()) return;

                foreach (var scope in scopes)
                {
                    context.Scopes.Add(scope.ToEntity());
                }

                context.SaveChanges();
            }
        }

        public static void SetupClients(IEnumerable<Client> clients,
            EntityFrameworkServiceOptions options)
        {
            using (var context =
                new ClientConfigurationDbContext(options.ConnectionString,
                    options.Schema))
            {
                if (context.Clients.Any()) return;

                foreach (var client in clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }
        }



    }
}