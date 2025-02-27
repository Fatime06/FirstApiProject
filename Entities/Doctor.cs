namespace WebApplication4.Entities
{
	public class Doctor : BaseEntity
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public decimal Salary { get; set; }
        public string Image { get; set; }
        public int DepartmentId { get; set; }
		public Department Department { get; set; }
	}
}
