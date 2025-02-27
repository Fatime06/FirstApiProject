namespace WebApplication4.Entities
{
	public class Book : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int PageCount { get; set; }
		public List<BookAuthor> BookAuthors { get; set; }
		public Book()
		{
			BookAuthors = new List<BookAuthor>();
		}
	}
}
