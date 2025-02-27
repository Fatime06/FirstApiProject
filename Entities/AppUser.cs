using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Entities
{
	public class AppUser : IdentityUser
	{
        public string FullName { get; set; }
    }
}
