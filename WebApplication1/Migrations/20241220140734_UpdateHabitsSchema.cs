using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHabitsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_Habits_HabitsId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_HabitsId",
                table: "Record");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Record_HabitsId",
                table: "Record",
                column: "HabitsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Habits_HabitsId",
                table: "Record",
                column: "HabitsId",
                principalTable: "Habits",
                principalColumn: "Id");
        }
    }
}
