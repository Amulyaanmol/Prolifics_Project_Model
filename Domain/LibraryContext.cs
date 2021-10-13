using Microsoft.EntityFrameworkCore;
using Model;


namespace Domain
{
    public class EFContext : DbContext
    {

        private const string connectionString = "Server=(localdb)\\ProjectsV13; Database = Test;Integrated security=True;Trusted_Connection=yes";
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Role> RoleSet { get; set; }

    }
    
}

    

