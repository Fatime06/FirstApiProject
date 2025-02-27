namespace WebApplication4.Dtos.Department
{
	public class DepartmentReturnDto
	{
		public string Name { get; set; }
		public int Capacity { get; set; }
		public List<DoctorInDepartmentReturnDto> Doctors { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public int DoctorsCount { get; set; }
	}
	public class DoctorInDepartmentReturnDto
	{
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
	}
}
