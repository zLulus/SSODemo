using IdentityServerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
//using DbContext = System.Data.Entity.DbContext;

namespace IdentityServerApi.EntityFramework
{
    public class SSOContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "server=115.28.102.108;uid=sa;pwd=Woshizenglu9501;database=SSODemoDb;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }

    }
}
