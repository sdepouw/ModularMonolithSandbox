using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books.Data;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
  internal static readonly Guid Book1Guid = new("1d665949-a89d-4a0a-aff9-6c8fee8be2b9");
  internal static readonly Guid Book2Guid = new("d0c59d6e-9df6-4dc8-a443-500b91abbc31");
  internal static readonly Guid Book3Guid = new("c305241e-5737-43a8-af67-6bd43be9bef7");

  public void Configure(EntityTypeBuilder<Book> builder)
  {
    builder.Property(p => p.Title)
      .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
      .IsRequired();

    builder.Property(p => p.Author)
      .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
      .IsRequired();

    builder.HasData(GetSampleBookData());
  }

  private IEnumerable<Book> GetSampleBookData()
  {
    const string tolkien = "J.R.R. Tolkien";
    yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 10.99m);
    yield return new Book(Book2Guid, "The Two Towers", tolkien, 11.99m);
    yield return new Book(Book3Guid, "The Return of the King", tolkien, 12.99m);
  }
}
