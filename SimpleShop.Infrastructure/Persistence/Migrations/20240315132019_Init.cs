﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleShop.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Orders",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
				UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
				IsPaid = table.Column<bool>(type: "bit", nullable: false),
				SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Orders", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Products",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
				ImageUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
				Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Products", x => x.Id);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Orders");

		migrationBuilder.DropTable(
			name: "Products");
	}
}
