using System.IO;
using System.Reflection;
using ATIS.Dal.Models;
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

        public DbSet<Tbl06Phylum> Tbl06Phylums { get; set; }
        public DbSet<Tbl09Division> Tbl09Divisions { get; set; }
        public DbSet<Tbl12Subphylum> Tbl12Subphylums { get; set; }
        public DbSet<Tbl15Subdivision> Tbl15Subdivisions { get; set; }
        public DbSet<Tbl18Superclass> Tbl18Superclasses { get; set; }

        public DbSet<Tbl90Reference> Tbl90References { get; set; }
        public DbSet<Tbl90RefAuthor> Tbl90RefAuthors { get; set; }
        public DbSet<Tbl90RefExpert> Tbl90RefExperts { get; set; }
        public DbSet<Tbl90RefSource> Tbl90RefSources { get; set; }
        public DbSet<Tbl93Comment> Tbl93Comments { get; set; }
        public DbSet<TblUserProfile> TblUserProfiles { get; set; }

    }


}
