using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class edit_method_parameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Class");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subject",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Instructor",
                newName: "InstructorId");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Class",
                newName: "DurationTo");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Class",
                newName: "DurationFrom");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Class",
                newName: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Subject",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "Instructor",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DurationTo",
                table: "Class",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "DurationFrom",
                table: "Class",
                newName: "From");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Class",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Class",
                nullable: false,
                defaultValue: false);
        }
    }
}
