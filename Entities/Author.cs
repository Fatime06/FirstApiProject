namespace WebApplication4.Entities
{
	public class Author : BaseEntity
	{
		public string Name { get; set; }
		public List<BookAuthor> BookAuthors { get; set; }
	}
}
