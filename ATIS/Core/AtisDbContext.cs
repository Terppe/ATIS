using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Ui.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core
{
    public class AtisDbContext : DbContext
    {
        public AtisDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            //connectionstring

            optionsbuilder.UseSqlServer(
                @"Data Source=W10LAPR3\SQLEXPRESS;Initial Catalog=ATIS34;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        }

        public DbSet<Tbl03Regnum> Tbl03Regnums { get; set; }

    }


}
