using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace raktarProgram.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            #region Admin Role
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "id",
                values: new object[] { "admin" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "Name",
                values: new object[] { "Admin" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "NormalizedName",
                values: new object[] { "ADMIN" });
            #endregion

            #region Leader Role
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "id",
                values: new object[] { "leader" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "Name",
                values: new object[] { "Leader" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "NormalizedName",
                values: new object[] { "LEADER" });
            #endregion

            #region Visitor Role
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "id",
                values: new object[] { "visitor" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "Name",
                values: new object[] { "Visitor" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                column: "NormalizedName",
                values: new object[] { "VISITOR" });
            #endregion

            var hasher = new PasswordHasher<AddRoles>();

            #region Adding Adming User
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                column: "id",
                values: new object[] { "1" });
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                column: "Name",
                values: new object[] { "bado.mate@outlook.com" });
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                column: "NormalizedName",
                values: new object[] { "BADO.MATE@OUTLOOK.COM" });
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                column: "Email",
                values: new object[] { "bado.mate@outlook.com" });
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                column: "Email",
                values: new object[] { true });
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                column: "PasswordHash",
                values: new object[] { hasher.HashPassword(null, "temporarypass") });
            #endregion

            #region Add Admin User Role
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                column: "UserId",
                values: new object[] { "1" });
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                column: "RoleId",
                values: new object[] { "admin" });
            #endregion

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValues: new object[] { "admin" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValues: new object[] { "leader" });

            migrationBuilder.DeleteData(
               table: "AspNetRoles",
               keyColumn: "id",
               keyValues: new object[] { "visitor" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValues: new object[] { "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumn: "UserId",
                keyValues: new object[] { "1" });

        }
    }
}
