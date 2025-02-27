using FluentValidation;

namespace WebApplication4.Dtos.Accounts
{
	public class RegisterDto
	{
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
	public class RegisterDtoValidator : AbstractValidator<RegisterDto>
	{
		public RegisterDtoValidator()
		{
			RuleFor(x => x.FullName).NotEmpty().MinimumLength(2).MaximumLength(20);
			RuleFor(x => x.UserName).NotEmpty().MinimumLength(2).MaximumLength(20);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
			RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
		}
	}
}
