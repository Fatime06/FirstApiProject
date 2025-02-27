namespace WebApplication4.Helpers
{
	public static class FileManager
	{
		public static string SaveImage(this IFormFile file, string folder)
		{
			string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
			string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, fileName);
			using FileStream fileStream = new(fullPath, FileMode.Create);
			file.CopyTo(fileStream);
			return fileName;
		}
		public static bool DeleteFile(string path)
		{
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
				return true;
			}
			return false;
		}
	}
}
