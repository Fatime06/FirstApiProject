using WebApplication4.Dtos.Doctor;
using WebApplication4.Entities;

namespace WebApplication4.Mappers
{
	public class DoctorMappers
	{
		public static Doctor DoctorCreateDtoToDoctor(DoctorCreateDto doctorCreateDto)
		{
			return new Doctor
			{
				Name = doctorCreateDto.Name,
				Surname = doctorCreateDto.Surname,
				Salary = doctorCreateDto.Salary,
				DepartmentId = doctorCreateDto.DepartmentId

			};
		}
		public static Doctor DoctorUpdateDtoToDoctor(DoctorUpdateDto doctorUpdateDto)
		{
			return new Doctor
			{
				Name = doctorUpdateDto.Name,
				Surname = doctorUpdateDto.Surname,
				Salary = doctorUpdateDto.Salary,
				DepartmentId = doctorUpdateDto.DepartmentId

			};
		}
	}
}
