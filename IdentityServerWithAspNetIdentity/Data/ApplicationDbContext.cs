using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServerWithAspNetIdentity.Models;

namespace IdentityServerWithAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //自己的表
        public DbSet<SendMessageLog> SendMessageLogs { get; set; }
        public DbSet<IdentityServer4Client> IdentityServer4Clients { get; set; }
        //identityserver的表
        //public DbSet<ApiResource> ApiResources { get; set; }
        //public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }
        //public DbSet<ApiScope> ApiScopes { get; set; }
        //public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        //public DbSet<ApiSecret> ApiSecrets { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<ClientClaim> ClientClaims { get; set; }
        //public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        //public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        //public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        //public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        //public DbSet<ClientProperty> ClientProperties { get; set; }
        //public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        //public DbSet<ClientScope> ClientScopes { get; set; }
        //public DbSet<ClientSecret> ClientSecrets { get; set; }
        //public DbSet<IdentityClaim> IdentityClaims { get; set; }
        //public DbSet<IdentityResource> IdentityResources { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
