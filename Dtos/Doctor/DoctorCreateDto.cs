using FluentValidation;

namespace WebApplication4.Dtos.Doctor
{
	public class DoctorCreateDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public decimal Salary { get; set; }
        public IFormFile Image { get; set; }
        public int DepartmentId { get; set; }
	}
	public class DoctorCreateDtoValidator : AbstractValidator<DoctorCreateDto>
	{
		public DoctorCreateDtoValidator()
		{
			RuleFor(d => d.Name)
				.NotEmpty().WithMessage("Name must not be empty")
				.MaximumLength(50).WithMessage("Name must be less than 50 characters");
			RuleFor(d => d.Surname)
				.NotEmpty().WithMessage("Name must not be empty")
				.MaximumLength(50).WithMessage("Name must be less than 50 characters");
			RuleFor(d => d).Custom((d, context) =>
			{
				if (d.Image == null)
				{
					context.AddFailure("Image", "Image is required");
				}
				if (d.Image.Length > 2 * 1024 * 1024)
				{
					context.AddFailure("Image", "Image must be less than 2 mb");
				}
				if (!d.Image.ContentType.Contains("image/"))
				{
					context.AddFailure("Image", "Image must be jpg or png");
				}
			});

		}
	}
}
