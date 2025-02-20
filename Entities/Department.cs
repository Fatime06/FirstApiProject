namespace WebApplication4.Entities
{
	public class Department : BaseEntity
	{
		public string Name { get; set; }
		public int Size { get; set; }
		public List<Doctor> Doctors { get; set; }
	}
}
