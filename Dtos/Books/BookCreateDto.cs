using FluentValidation;

namespace WebApplication4.Dtos.Books
{
	public class BookCreateDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int PageCount { get; set; }
		public int[] AuthorIds { get; set; }
	}
	public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
	{
		public BookCreateDtoValidator()
		{
			RuleFor(x => x.Title).NotEmpty()
				.MaximumLength(40)
				.MinimumLength(4);
			RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
			RuleFor(x => x.PageCount).InclusiveBetween(1, 900);
			RuleFor(x => x.AuthorIds).NotEmpty();
		}
	}
}
