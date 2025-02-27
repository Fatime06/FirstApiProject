using AutoMapper;
using WebApplication4.Dtos.Books;
using WebApplication4.Dtos.Department;
using WebApplication4.Dtos.Doctor;
using WebApplication4.Entities;
using WebApplication4.Helpers;

namespace WebApplication4.Profiles
{
	public class MapperProfile : Profile
	{
		private readonly IHttpContextAccessor _contextAccessor;
		public MapperProfile(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
			var httpContext = _contextAccessor.HttpContext;
			var uriBuilder = new UriBuilder(httpContext.Request.Scheme, httpContext.Request.Host.Host, (int)httpContext.Request.Host.Port);
			var url = uriBuilder.Uri.AbsoluteUri;




			CreateMap<Department, DepartmentReturnDto>();
			CreateMap<Doctor, DoctorInDepartmentReturnDto>();

			CreateMap<Doctor, DoctorReturnDto>()
				.ForMember(d => d.Image, opt => opt.MapFrom(s => url + "uploads/" + s.Image));
			CreateMap<Department, DepartmentInDoctorReturnDto>();
			CreateMap<DoctorCreateDto, Doctor>()
				.ForMember(d => d.Image, opt => opt.MapFrom(s => s.Image.SaveImage("uploads")));
			CreateMap<DoctorUpdateDto, Doctor>()
			 .ForMember(d => d.Image, opt =>
			 {
				 opt.Condition(s => s.Image != null);
				 opt.MapFrom(s => s.Image.SaveImage("uploads"));
			 });
			CreateMap<BookCreateDto, Book>();
			CreateMap<Book, BookReturnDto>()
				.ForMember(dest => dest.BookAuthors, opt => opt.MapFrom(s => s.BookAuthors.Select(ba => ba.Author).ToList()));

			CreateMap<Author, AuthorInBookDto>();
		}
	}
}
