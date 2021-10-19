using Microsoft.EntityFrameworkCore;
using Model;


namespace Domain
{
    public class PpmContext : DbContext
    {
        public virtual DbSet<Role> RoleEF { get; set; }
        private const string connectionString = "Server=(localdb)\\ProjectsV13; Database = Test;Integrated security=True;Trusted_Connection=yes";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }
        
    }
}
