using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ABMS;Trusted_Connection=true");
            optionsBuilder.UseSqlServer("Server=tcp:abmsdb.database.windows.net,1433;Initial Catalog=ABMS;Persist Security Info=False;User ID=meb;Password=Enes125@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=6;");
        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ApartmentComplex> ApartmentComplexes { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Bill> Bills { get; set; }


    }
}
