using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model.EntityFramework
{
    public class SSOContext : DbContext
    {
        private IConfiguration Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //读取配置文件appsettings.json
            //todo 
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json");
            //Configuration = builder.Build();
            string sqlserverConnection = Configuration.GetConnectionString("SSODemoConnection");
            optionsBuilder.UseSqlServer("User ID=sa;Password=Woshizenglu9501;Host=115.28.102.108;Database=SSODemoDb;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

    }
}
