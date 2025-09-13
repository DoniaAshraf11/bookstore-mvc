using Microsoft.EntityFrameworkCore;
using BookStoreMVC.Models;

namespace BookStoreMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Donia\\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
