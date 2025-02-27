using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Dtos.Books;
using WebApplication4.Entities;

namespace WebApplication4.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly MedicineDbContext _context;
		private readonly IMapper _mapper;

		public BookController(MedicineDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		[HttpGet("")]
		public IActionResult Get()
		{
			var books = _context.Books
				.Include(b => b.BookAuthors)
				.ThenInclude(ba => ba.Author)
				.ToList();
			return Ok(_mapper.Map<List<BookReturnDto>>(books));
		}
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var book = _context.Books
				.Include(b => b.BookAuthors)
				.ThenInclude(ba => ba.Author)
				.FirstOrDefault(x => x.Id == id);
			if (book is null) return StatusCode(StatusCodes.Status404NotFound);
			return Ok(_mapper.Map<BookReturnDto>(book));
		}
		[HttpPost("")]
		public IActionResult Create([FromForm] BookCreateDto createDto)
		{
			Book book = _mapper.Map<Book>(createDto);
			foreach (var authorId in createDto.AuthorIds)
			{
				if (!_context.Authors.Any(a => a.Id == authorId)) return StatusCode(StatusCodes.Status409Conflict);
				BookAuthor bookAuthor = new BookAuthor();
				bookAuthor.AuthorId = authorId;
				bookAuthor.BookId = book.Id;
				book.BookAuthors.Add(bookAuthor);
			}

			_context.Books.Add(book);
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}
