using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXPERMIN.DATABASE.Migrations
{
    /// <inheritdoc />
    public partial class uploadbanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Banners");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaFileId",
                table: "Banners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MediaFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTemporary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_MediaFileId",
                table: "Banners",
                column: "MediaFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_MediaFile_MediaFileId",
                table: "Banners",
                column: "MediaFileId",
                principalTable: "MediaFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_MediaFile_MediaFileId",
                table: "Banners");

            migrationBuilder.DropTable(
                name: "MediaFile");

            migrationBuilder.DropIndex(
                name: "IX_Banners_MediaFileId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "MediaFileId",
                table: "Banners");

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
