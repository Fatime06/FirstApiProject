using FluentValidation;

namespace WebApplication4.Dtos.Accounts
{
	public class LoginDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
	public class LoginDtoValidator : AbstractValidator<LoginDto>
	{
		public LoginDtoValidator()
		{
			RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
			RuleFor(x => x.UserName).NotEmpty().MinimumLength(2).MaximumLength(20);
		}
	}
}
