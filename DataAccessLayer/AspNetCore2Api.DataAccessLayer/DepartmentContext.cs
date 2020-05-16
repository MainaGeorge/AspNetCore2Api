﻿using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApi.DataAccessLayer
{
    public class DepartmentContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ASPApiDepartments;Trusted_Connection=True");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
