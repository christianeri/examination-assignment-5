using Microsoft.EntityFrameworkCore;
using SupportTicketManager.Models.Entities;

namespace SupportTicketManager.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\e.christian\Desktop\DEV\VS\repos\DATLAGR30\Examination-assignment-5\SupportTicketManager\Contexts\SupportTicketManager_db.mdf;Integrated Security=True;Connect Timeout=30";

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<StatusEntity>().HasData(
                new { Id = 1, TicketStatus = "Ej Påbörjad" },
                new { Id = 2, TicketStatus = "Pågående" },
                new { Id = 3, TicketStatus = "Avslutad" }
                );


            modelBuilder.Entity<BuildingEntity>().HasData(
                new { Id = 1, BuildingName = "Blåsenhus", PropertyCode = "5:1" },
                new { Id = 2, BuildingName = "Carolina Rediviva", PropertyCode = "1:68" },
                new { Id = 3, BuildingName = "Ekonomikum", PropertyCode = "62:8" },
                new { Id = 4, BuildingName = "Rudbecklaboratoriet", PropertyCode = "1:23" },
                new { Id = 5, BuildingName = "Ångströmlaboratoriet", PropertyCode = "7:1" }
                );
        }

        public DbSet<TicketEntity> Tickets { get; set; } = null!;  
        public DbSet<StatusEntity> Statuses { get; set; } = null!;  
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<BuildingEntity> Buildings { get; set; } = null!;
    }
}
