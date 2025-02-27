using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Dtos.Doctor;
using WebApplication4.Entities;
using WebApplication4.Helpers;
using WebApplication4.Mappers;

namespace HospitalApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly MedicineDbContext _context;
		private readonly IMapper _mapper;

		public DoctorController(MedicineDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var doctors = _context.Doctors.Include(d => d.Department).ThenInclude(d => d.Doctors).ToList();
			var doctorsReturnDto = _mapper.Map<List<DoctorReturnDto>>(doctors);
			return Ok(doctorsReturnDto);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var existDoctor = _context.Doctors.Include(d => d.Department).ThenInclude(d => d.Doctors).FirstOrDefault(d => d.Id == id);
			if (existDoctor is null) return StatusCode(StatusCodes.Status404NotFound);
			var returnDto = _mapper.Map<DoctorReturnDto>(existDoctor);
			return Ok(returnDto);
		}
		[HttpPost]
		public IActionResult Create([FromForm] DoctorCreateDto createDto)
		{
			if (!_context.Departments.Any(d => d.Id == createDto.DepartmentId)) return StatusCode(StatusCodes.Status404NotFound);
			_context.Doctors.Add(_mapper.Map<Doctor>(createDto));
			_context.SaveChanges();
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpPut("{id}")]
		public IActionResult Update(int? id, [FromForm] DoctorUpdateDto updateDto)
		{
			if (id is null) return BadRequest();
			var existDoctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
			if (existDoctor is null) return StatusCode(StatusCodes.Status404NotFound);
			if ((!_context.Departments.Any(d => d.Id == updateDto.DepartmentId)) && existDoctor.DepartmentId != updateDto.DepartmentId)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			if (updateDto.Image != null)
			{
				string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", existDoctor.Image);
				FileManager.DeleteFile(path);
			}
			existDoctor = _mapper.Map(updateDto, existDoctor);
			_context.SaveChanges();
			return Ok();
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var existDoctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
			if (existDoctor is null) return StatusCode(StatusCodes.Status404NotFound);
			_context.Doctors.Remove(existDoctor);
			_context.SaveChanges();
			return Ok();
		}
	}
}
