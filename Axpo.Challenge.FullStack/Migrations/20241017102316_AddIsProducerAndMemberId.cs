using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Axpo.Challenge.FullStack.Migrations
{
    /// <inheritdoc />
    public partial class AddIsProducerAndMemberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForecastData_Members_MemberId",
                table: "ForecastData");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_BalancingCircles_BalancingCircleId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForecastData",
                table: "ForecastData");

            migrationBuilder.RenameTable(
                name: "ForecastData",
                newName: "Forecasts");

            migrationBuilder.RenameIndex(
                name: "IX_ForecastData_MemberId",
                table: "Forecasts",
                newName: "IX_Forecasts_MemberId");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "TimeseriesEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BalancingCircleId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProducer",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forecasts",
                table: "Forecasts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeseriesEntries_MemberId",
                table: "TimeseriesEntries",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_Members_MemberId",
                table: "Forecasts",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_BalancingCircles_BalancingCircleId",
                table: "Members",
                column: "BalancingCircleId",
                principalTable: "BalancingCircles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeseriesEntries_Members_MemberId",
                table: "TimeseriesEntries",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forecasts_Members_MemberId",
                table: "Forecasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_BalancingCircles_BalancingCircleId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeseriesEntries_Members_MemberId",
                table: "TimeseriesEntries");

            migrationBuilder.DropIndex(
                name: "IX_TimeseriesEntries_MemberId",
                table: "TimeseriesEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forecasts",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "TimeseriesEntries");

            migrationBuilder.DropColumn(
                name: "IsProducer",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Forecasts",
                newName: "ForecastData");

            migrationBuilder.RenameIndex(
                name: "IX_Forecasts_MemberId",
                table: "ForecastData",
                newName: "IX_ForecastData_MemberId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BalancingCircleId",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForecastData",
                table: "ForecastData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForecastData_Members_MemberId",
                table: "ForecastData",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_BalancingCircles_BalancingCircleId",
                table: "Members",
                column: "BalancingCircleId",
                principalTable: "BalancingCircles",
                principalColumn: "Id");
        }
    }
}
