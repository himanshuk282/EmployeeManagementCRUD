using EmployeeManagement_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_API.Data
{
    public class EmployeeManagementDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeeManagementDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
