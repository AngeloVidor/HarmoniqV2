using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBoolProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPartOfAlbum",
                table: "Musics");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumId",
                table: "Musics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumId",
                table: "Musics",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsPartOfAlbum",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
