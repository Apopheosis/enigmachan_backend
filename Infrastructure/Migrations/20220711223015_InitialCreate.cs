using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace enigmachan_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    key = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    post_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    thread_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    post_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image_urls = table.Column<string[]>(type: "text[]", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    postDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    reply_to = table.Column<List<string>>(type: "text[]", nullable: true),
                    replies = table.Column<List<string>>(type: "text[]", nullable: true),
                    bumps = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.post_id);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    post_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image_urls = table.Column<string[]>(type: "text[]", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    postDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    reply_to = table.Column<List<string>>(type: "text[]", nullable: true),
                    replies = table.Column<List<string>>(type: "text[]", nullable: true),
                    mainPostId = table.Column<long>(type: "bigint", nullable: false),
                    Threadpost_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_Replies_Threads_Threadpost_id",
                        column: x => x.Threadpost_id,
                        principalTable: "Threads",
                        principalColumn: "post_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_post_id",
                table: "Posts",
                column: "post_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_post_id",
                table: "Replies",
                column: "post_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_Threadpost_id",
                table: "Replies",
                column: "Threadpost_id");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_post_id",
                table: "Threads",
                column: "post_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Threads");
        }
    }
}
