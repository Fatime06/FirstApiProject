namespace WebApplication4.Dtos.Books
{
	public class BookReturnDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int PageCount { get; set; }
		public List<AuthorInBookDto> BookAuthors { get; set; }
	}
	public class AuthorInBookDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
	}
}
