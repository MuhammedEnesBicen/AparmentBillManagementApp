using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ABMS;Trusted_Connection=true");

        }

        public DbSet<Apartment> Apartments { get;set; }
        public DbSet<Tenant> Tenants {get;set;}
        public DbSet<Message> Messages {get;set;}
        public DbSet<Bill> Bills {get;set;}


    }
}
