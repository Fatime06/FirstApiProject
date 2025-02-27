namespace WebApplication4.Dtos.Doctor
{
	public class DoctorReturnDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }
		public string Image { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public int DepartmentId { get; set; }
		public DepartmentInDoctorReturnDto Department { get; set; }
	}
	public class DepartmentInDoctorReturnDto
	{
		public string Name { get; set; }
		public int DoctorsCount { get; set; }
	}
}
