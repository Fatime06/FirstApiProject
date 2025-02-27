using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Entities;

namespace WebApplication4.Data
{
	public class MedicineDbContext : IdentityDbContext<AppUser>
	{
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
		public MedicineDbContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicineDbContext).Assembly);


			var userId = Guid.NewGuid().ToString();
			var memberRoleId = Guid.NewGuid().ToString();
			var adminRoleId = Guid.NewGuid().ToString();
			var superAdminRoleId = Guid.NewGuid().ToString();
			modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole
				{
					Id = adminRoleId,
					Name = "Admin",
					NormalizedName = "ADMIN".ToUpper()
				},
				new IdentityRole
				{
					Id = memberRoleId,
					Name = "Member",
					NormalizedName = "MEMBER".ToUpper()
				},
				new IdentityRole
				{
					Id = superAdminRoleId,
					Name = "SuperAdmin",
					NormalizedName = "SUPERADMIN".ToUpper()
				}
				);
			var hasher = new PasswordHasher<IdentityUser>();
			modelBuilder.Entity<AppUser>().HasData(
				new AppUser
				{
					Id = userId,
					FullName = "User Name",
					UserName = "_user",
					NormalizedUserName = "_USER",
					Email = "user@gmail.com",
					NormalizedEmail = "USER@GMAIL.COM",
					PasswordHash = hasher.HashPassword(null, "12345@Ff")
				}
				);
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string>
				{
					UserId = userId,
					RoleId = adminRoleId
				}
				);
			base.OnModelCreating(modelBuilder);
		}
	}
}
