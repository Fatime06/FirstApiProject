namespace WebApplication4.Dtos.Doctor
{
	public class DoctorCreateDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public decimal Salary { get; set; }
		public int DepartmentId { get; set; }
	}
}
