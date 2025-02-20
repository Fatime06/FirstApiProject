using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Dtos.Department;
using WebApplication4.Entities;

namespace HospitalApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly MedicineDbContext _context;

		public DepartmentController(MedicineDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Departments.ToList());
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var existDepartment = _context.Departments.FirstOrDefault(d => d.Id == id);
			if (existDepartment is null) return StatusCode(StatusCodes.Status404NotFound);
			return Ok(existDepartment);
		}
		[HttpPost]
		public IActionResult Create(DepartmentCreateDto createDto)
		{
			if (_context.Departments.Any(d => d.Name.ToLower() == createDto.Name.Trim().ToLower()))
				return StatusCode(StatusCodes.Status409Conflict);
			Department department = new()
			{
				Name = createDto.Name,
				Size = createDto.Size,
				CreatedDate = DateTime.Now
			};
			_context.Departments.Add(department);
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var existDepartment = _context.Departments.FirstOrDefault(d => d.Id == id);
			if (existDepartment is null) return StatusCode(StatusCodes.Status404NotFound);
			_context.Departments.Remove(existDepartment);
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status200OK);
		}
		[HttpPut("{id}")]
		public IActionResult Update(int id, DepartmentUpdateDto updateDto)
		{
			if (_context.Departments.Any(d => d.Name.ToLower() == updateDto.Name.Trim().ToLower() && d.Id != id))
				return StatusCode(StatusCodes.Status409Conflict);
			var existDepartment = _context.Departments.FirstOrDefault(d => d.Id == id);
			if (existDepartment is null) return StatusCode(StatusCodes.Status404NotFound);
			existDepartment.Name = updateDto.Name;
			existDepartment.Size = updateDto.Size;
			existDepartment.ModifiedDate = DateTime.Now;
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status200OK);
		}
	}
}
