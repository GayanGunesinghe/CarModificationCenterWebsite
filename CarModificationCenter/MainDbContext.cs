using System.Data.Entity;
using CarModificationCenter.Models;

namespace CarModificationCenter
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(): base("name=DefaultConnection")  //linking of the db.
        {
        }

        public DbSet<website_users> Users { get; set; }     //specifies the table.
        public DbSet<website_vehicles> Vehicles { get; set; }
        public DbSet<project> Project { get; set; }
    }
}