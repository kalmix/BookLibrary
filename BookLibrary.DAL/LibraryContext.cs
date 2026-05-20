using BookLibrary.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DigitalLibraryDb;Trusted_Connection=True;");
        }
    }
}
