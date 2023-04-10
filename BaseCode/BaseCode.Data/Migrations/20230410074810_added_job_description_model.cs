using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class added_job_description_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobDescription",
                table: "JobRequirement",
                newName: "Requirement");

            migrationBuilder.CreateTable(
                name: "JobDescription",
                columns: table => new
                {
                    JobDescriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescription", x => x.JobDescriptionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobDescription");

            migrationBuilder.RenameColumn(
                name: "Requirement",
                table: "JobRequirement",
                newName: "JobDescription");
        }
    }
}
