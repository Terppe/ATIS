using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ATIS.Ui.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ATIS.Ui.Core
{
    public class AtisDbContext : DbContext
    {
        private readonly string _connectionString;

        public AtisDbContext() : base()
        {
            var builder = new ConfigurationBuilder();
            //builder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location));
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("MyDbConnection").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{
        //    //connectionstring

        //    optionsbuilder?.UseSqlServer(
        //        @"Data Source=W10LAPR3\SQLEXPRESS;Initial Catalog=ATIS34;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        //}

        //connectionstring place extern later


        public DbSet<Tbl03Regnum> Tbl03Regnums { get; set; }

    }


}
