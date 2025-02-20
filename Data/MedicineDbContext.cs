using Microsoft.EntityFrameworkCore;
using WebApplication4.Entities;

namespace WebApplication4.Data
{
	public class MedicineDbContext : DbContext
	{
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public MedicineDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
