using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication4.Dtos.Accounts;
using WebApplication4.Entities;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IOptions<JwtSetting> _setting;
		private readonly JwtService _service;
		private readonly EmailService _emailService;

		public AccountController(UserManager<AppUser> userManager, IOptions<JwtSetting> jwtSetting, JwtService jwtService, EmailService emailService)
		{
			_userManager = userManager;
			_setting = jwtSetting;
			_service = jwtService;
			_emailService = emailService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto dto)
		{
			AppUser user = await _userManager.FindByNameAsync(dto.UserName);
			if (user is not null) return BadRequest("User already exists");
			user = new()
			{
				FullName = dto.FullName,
				Email = dto.Email,
				UserName = dto.UserName
			};
			var result = await _userManager.CreateAsync(user, dto.Password);
			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}
			await _userManager.AddToRoleAsync(user, "member");
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var uriBuilder = new UriBuilder(HttpContext.Request.Scheme, HttpContext.Request.Host.Host, (int)HttpContext.Request.Host.Port);
			var url = uriBuilder.Uri.AbsoluteUri + "api/account/confirmEmail?email=" + user.Email + "&token=" + token;
			string body = $"<a href='{url}'>Click to verify</a>";
			_emailService.SendEmail(user.Email, "Confirm Email", body);
			return Ok();
		}
		[HttpGet("confirmEmail")]
		public IActionResult ConfirmEmail(string email, string token)
		{
			var user = _userManager.FindByEmailAsync(email).Result;
			if (user is null) return BadRequest("User not found");
			var result = _userManager.ConfirmEmailAsync(user, token).Result;
			if (!result.Succeeded) return BadRequest(result.Errors);
			return Ok();
		}
		[HttpGet("resetpassword")]
		public IActionResult ForgotPassword(string email)
		{
			var user = _userManager.FindByEmailAsync(email).Result;
			if (user is null) return BadRequest("User not found");
			var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
			var uriBuilder = new UriBuilder(HttpContext.Request.Scheme, HttpContext.Request.Host.Host, (int)HttpContext.Request.Host.Port);
			var url = uriBuilder.Uri.AbsoluteUri + "api/account/resetpassword?email=" + user.Email + "&token=" + token;
			string body = $"<a href='{url}'>Click to reset password</a>";
			_emailService.SendEmail(user.Email, "Reset Password", body);
			return Ok();
		}
		[HttpPost("resetpassword")]
		public IActionResult ResetPassword(string token, string password, string email)
		{
			var user = _userManager.FindByEmailAsync(email).Result;
			if (user is null) return BadRequest("User not found");
			var result = _userManager.ResetPasswordAsync(user, token, password).Result;
			if (!result.Succeeded) return BadRequest(result.Errors);
			return Ok();
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			JwtSetting jwt = _setting.Value;

			var user = await _userManager.FindByNameAsync(dto.UserName);
			if (user is null) return StatusCode(StatusCodes.Status404NotFound);
			if (!user.EmailConfirmed) return BadRequest("Email not confirmed");
			var result = await _userManager.CheckPasswordAsync(user, dto.Password);
			if (!result) return BadRequest("Password or username is incorrect");

			var roles = await _userManager.GetRolesAsync(user);

			return Ok(new { token = _service.GetToken(user, roles, jwt) });
		}
	}
}
