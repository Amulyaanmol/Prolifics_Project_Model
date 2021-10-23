using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class PpmContext : DbContext
    {
        public virtual DbSet<Role> RoleEf { get; set; }
        public virtual DbSet<Project> ProjectEf { get; set; }
        public virtual DbSet<Employee> EmployeeEf { get; set; }
       

        private const string connectionString = "Server=(localdb)\\ProjectsV13; Database = TestDb;Integrated security=True;Trusted_Connection=yes";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
