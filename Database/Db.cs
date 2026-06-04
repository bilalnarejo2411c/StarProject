using Microsoft.EntityFrameworkCore;
using Star_Project.Models;

namespace Star_Project.Database
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Hiring> Hirings{ get; set; }
        public DbSet<Star_Project.Models.AdminRegistration> AdminRegistration { get; set; } = default!;
    }
}