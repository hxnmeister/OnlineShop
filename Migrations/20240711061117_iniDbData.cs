using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Migrations
{
    public partial class iniDbData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
                (
                    table: "Books",
                    columns: ["Id",
                        "Title",
                        "PublisherName",
                        "Genre",
                        "Rating",
                        "PagesAmount",
                        "Price",
                        "PreviousPrice",
                        "Author",
                        "ImageUrl"],
                    values: new object[,]
                    {
                        { 1, "1984", "Ranok", "Sci-Fi", 5, 324, 20.99, 43.99, "F. Scott Fitzgerald",
                            "https://static.yakaboo.ua/media/catalog/product/2/6/26_1_329.jpg" },
                        { 2, "To Kill a Mockingbird", "Svitanok", "Romance", 3, 512, 5.99, 0, "Harper Lee",
                            "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg/800px-To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg" },
                        { 3, "The Great Gatsby", "New York Times", "Tragedy", 4, 211, 5.99, 10.99, "George Orwell",
                            "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg/800px-The_Great_Gatsby_Cover_1925_Retouched.jpg" },
                        { 4, "Harry Potter and the Sorcerer's Stone", "Ranok", "Fiction", 5, 981, 7.99, 0, "F. Scott Fitzgerald",
                            "https://m.media-amazon.com/images/I/91wKDODkgWL._SL1500_.jpg"},
                        { 5, "Jane Eyre", "Svitanok", "Romance", 2, 312, 3.99, 20.29, "George Orwell",
                            "https://m.media-amazon.com/images/I/91zU70Aw9IS._SL1500_.jpg"}
                    }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData
                (
                    table: "Books",
                    keyColumn: "Id",
                    keyValues: [1, 2, 3, 4, 5]
                );
        }
    }
}
