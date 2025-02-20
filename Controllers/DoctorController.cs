using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Dtos.Doctor;
using WebApplication4.Entities;

namespace HospitalApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly MedicineDbContext _context;

		public DoctorController(MedicineDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Doctors.Include(d => d.Department).ToList());
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var existDepartment = _context.Doctors.Include(d => d.Department).FirstOrDefault(d => d.Id == id);
			if (existDepartment is null) return StatusCode(StatusCodes.Status404NotFound);
			return Ok(existDepartment);
		}
		[HttpPost]
		public IActionResult Create(DoctorCreateDto createDto)
		{
			if (!_context.Departments.Any(d => d.Id == createDto.DepartmentId)) return StatusCode(StatusCodes.Status404NotFound);
			Doctor doctor = new()
			{
				Name = createDto.Name,
				Surname = createDto.Surname,
				CreatedDate = DateTime.Now,
				DepartmentId = createDto.DepartmentId
			};
			_context.Doctors.Add(doctor);
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var existDoctor = _context.Departments.FirstOrDefault(d => d.Id == id);
			if (existDoctor is null) return StatusCode(StatusCodes.Status404NotFound);
			_context.Departments.Remove(existDoctor);
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status200OK);
		}
		[HttpPut("{id}")]
		public IActionResult Update(int id, DoctorUpdateDto updateDto)
		{
			if (!_context.Departments.Any(d => d.Id == updateDto.DepartmentId)) return StatusCode(StatusCodes.Status404NotFound);
			var existDoctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
			if (existDoctor is null) return StatusCode(StatusCodes.Status404NotFound);
			existDoctor.Name = updateDto.Name;
			existDoctor.Surname = updateDto.Surname;
			existDoctor.ModifiedDate = DateTime.Now;
			existDoctor.DepartmentId = updateDto.DepartmentId;
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status200OK);
		}
	}
}
