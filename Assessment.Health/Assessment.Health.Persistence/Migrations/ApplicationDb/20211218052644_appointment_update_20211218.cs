using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Health.Persistence.Migrations.ApplicationDb
{
    public partial class appointment_update_20211218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialisation",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SymptomName",
                table: "Appointments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialisation",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SymptomName",
                table: "Appointments");
        }
    }
}
