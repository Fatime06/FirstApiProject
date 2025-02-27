using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication4.Entities;

namespace WebApplication4.Data.Configurations
{
	public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
	{
		public void Configure(EntityTypeBuilder<BookAuthor> builder)
		{
			builder.HasKey(ba => new { ba.BookId, ba.AuthorId });


			builder.HasOne(ba => ba.Book)
				.WithMany(b => b.BookAuthors)
				.HasForeignKey(b => b.BookId);

			builder.HasOne(ba => ba.Author)
				.WithMany(b => b.BookAuthors)
				.HasForeignKey(b => b.AuthorId);

		}
	}
}
