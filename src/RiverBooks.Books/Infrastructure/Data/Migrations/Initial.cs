#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RiverBooks.Books.Infrastructure.Data.Migrations
{
  /// <inheritdoc />
  public partial class Initial : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.EnsureSchema(
          name: "Books");

      migrationBuilder.CreateTable(
          name: "Books",
          schema: "Books",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Price = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Books", x => x.Id);
          });

      migrationBuilder.InsertData(
          schema: "Books",
          table: "Books",
          columns: new[] { "Id", "Author", "Price", "Title" },
          values: new object[,]
          {
                    { new Guid("1d665949-a89d-4a0a-aff9-6c8fee8be2b9"), "J.R.R. Tolkien", 10.99m, "The Fellowship of the Ring" },
                    { new Guid("c305241e-5737-43a8-af67-6bd43be9bef7"), "J.R.R. Tolkien", 12.99m, "The Return of the King" },
                    { new Guid("d0c59d6e-9df6-4dc8-a443-500b91abbc31"), "J.R.R. Tolkien", 11.99m, "The Two Towers" }
          });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Books",
          schema: "Books");
    }
  }
}
