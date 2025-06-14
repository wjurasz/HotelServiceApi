using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelService.Client.Storage.Migrations
{
    /// <inheritdoc />
    public partial class ClientSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Hotel",
                table: "Clients",
                columns: new[] { "FirstName", "LastName", "Email", "PhoneNumber" },
                values: new object[,]
                {
                    { "Anna", "Kowalska", "anna.kowalska@example.com", "123456789" },
                    { "Jan", "Nowak", "jan.nowak@example.com", "987654321" },
                    { "Ewa", "Zając", "ewa.zajac@example.com", "456123789" },
                    { "Piotr", "Wiśniewski", "piotr.w@example.com", "654321987" },
                    { "Dawid", "Wójcik", "d.wojcik@example.com", "321654987" },
                    { "Ryszard", "Wójcik", "r.Wójcik@example.com", "147258369" },
                    { "Katarzyna", "Lewandowska", "kasia.lew@example.com", "963852741" },
                    { "Marek", "Krawczyk", "marek.krawczyk@example.com", "852741963" },
                    { "Julia", "Mazur", "j.mazur@example.com", "741852963" },
                    { "Paweł", "Szymański", "pawel.szymanski@example.com", "159357486" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Usuwa wszystkie rekordy w razie rollbacka
            migrationBuilder.Sql("DELETE FROM [Hotel].[Clients]");
        }
    }
}
