using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication4.Entities;

namespace WebApplication4.Data.Configurations
{
	public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(d => d.Name).IsRequired().HasMaxLength(30);
			builder.Property(d => d.Size).IsRequired();
			builder.HasMany(d => d.Doctors).WithOne(d => d.Department).HasForeignKey(d => d.DepartmentId);
		}
	}
}
