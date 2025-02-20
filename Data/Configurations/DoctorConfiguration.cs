using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication4.Entities;

namespace WebApplication4.Data.Configurations
{
	public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
	{
		public void Configure(EntityTypeBuilder<Doctor> builder)
		{
			builder.Property(d => d.Name).IsRequired().HasMaxLength(30);
			builder.Property(d => d.Surname).IsRequired().HasMaxLength(40);
			builder.Property(d => d.Salary).IsRequired().HasColumnType("decimal(18,2)");
		}
	}
}
