using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Models;

namespace My_Blog_Website.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Authors> authors { get; set; }
        public DbSet<Contacts> contacts { get; set; }
    }
}
