using AspNetCoreApi.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore2Api.DataAccessLayer
{
    public class DepartmentContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ASPApiDepartments;Trusted_Connection=True");
        }

        public DbSet<Department> Departments { get; set; }
    }
}
