using FluentValidation.AspNetCore;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Dtos.Doctor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication4.Entities;
using WebApplication4.Profiles;
using WebApplication4.Services;

namespace WebApplication4
{
	public static class ServiceRegistration
	{
		public static void RegisterService(this IServiceCollection services, IConfiguration config)
		{
			services.AddControllers()
	.AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
			services.AddDbContext<MedicineDbContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddFluentValidationAutoValidation();
			services.AddFluentValidationClientsideAdapters();
			services.AddValidatorsFromAssemblyContaining<DoctorUpdateDtoValidator>();
			services.AddFluentValidationRulesToSwagger();
			services.AddHttpContextAccessor();
			services.AddAutoMapper(options =>
			{
				options.AddProfile(new MapperProfile(new HttpContextAccessor()));
			});
			services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
				opt.Password.RequireDigit = true;
				opt.Password.RequireNonAlphanumeric = true;
				opt.Password.RequireLowercase = true;
				opt.Password.RequireUppercase = true;
				opt.Password.RequiredLength = 6;
				opt.User.RequireUniqueEmail = true;

				opt.SignIn.RequireConfirmedEmail = true;



				opt.Lockout.MaxFailedAccessAttempts = 3;
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
				opt.Lockout.AllowedForNewUsers = true;
			}).AddEntityFrameworkStores<MedicineDbContext>().AddDefaultTokenProviders();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = config.GetSection("Jwt:Issuer").Value,
					ValidAudience = config.GetSection("Jwt:Audience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value)),
					ClockSkew = TimeSpan.Zero
				};
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "JWTToken_Auth_API",
					Version = "v1"
				});
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
	{
		new OpenApiSecurityScheme {
			Reference = new OpenApiReference {
				Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
			}
		},
		new string[] {}
	}
});
			});
			services.Configure<JwtSetting>(config.GetSection("Jwt"));
			services.AddAuthorization();
			services.AddScoped<JwtService>();
			services.AddScoped<EmailService>();
		}
	}
}
