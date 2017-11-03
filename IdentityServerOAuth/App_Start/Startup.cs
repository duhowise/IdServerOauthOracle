using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.EntityFramework;
using IdentityServerOAuth;
using IdentityServerOAuth.Entities;
using IdentityServerOAuth.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServerOAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", id =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IdServerOauth"].ConnectionString;

                id.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "BICT Identity Server",
                    SigningCertificate = LoadCertificate(),
                    RequireSsl = false,
                    Factory = new IdentityServerServiceFactory().Configure(connectionString)
                    
                });
                app.Map("/admin", adminApp => {
                    adminApp.UseIdentityManager(new IdentityManagerOptions()
                    {
                        Factory = new IdentityManagerServiceFactory().Configure(connectionString)
                    });
                });
            });
        }
      

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\{
                        ConfigurationManager.AppSettings["certificateName"]
                    }", ConfigurationManager.AppSettings["certificatePass"]);
        }
    }
}
