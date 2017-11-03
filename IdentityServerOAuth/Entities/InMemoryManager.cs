using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityServerOAuth.Entities
{
    public class InMemoryManager
    {
        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "mail@duhp.se",
                    Username = "mail@duhp.se",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.Name, "duhp Ekberg"),
                        new Claim(Constants.ClaimTypes.Scope, "read"),
                    }
                }
            };
        }

        public static IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read User Data"
                },
                new Scope {
                    Name = "identity_manager",
                    DisplayName = "Identity Manager",
                    Description = "Authorization for Identity Manager",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim> {
                        new ScopeClaim(Constants.ClaimTypes.Name),
                        new ScopeClaim(Constants.ClaimTypes.Role)
                    }
                }

            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "SocialNetwork",
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.OfflineAccess,
                        "read"
                    },
                    Enabled = true
                },
                new Client
                {
                    ClientId = "socialnetwork_implicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "SocialNetwork",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:28037",
                        "http://localhost:28037/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:28037"
                    },
                    Enabled = true
                } ,
                new Client
                {
                    ClientId = "socialnetwork_code",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "SocialNetwork",
                    Flow = Flows.Hybrid,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.OfflineAccess,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                       
                        "http://localhost:28037/"
                        
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:28037"
                    },
                    Enabled = true
                },
                new Client {
                    ClientId = "identity_manager",
                    ClientName = "Identity Manager",
                    Enabled = true,
                    RedirectUris = new List<string> {"https://localhost:44307/"},
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    AllowedScopes = new List<string> {
                        Constants.StandardScopes.OpenId,
                        "identity_manager"
                    }
                }
            };
        }
    }
}