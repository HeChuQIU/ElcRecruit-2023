using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace interviewer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Interviewers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviewers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    College = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeChat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstDepartment = table.Column<int>(type: "int", nullable: false),
                    SecondDepartment = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "123e1f3b-df90-481b-9888-cbc701878782", "605017f2-d995-4cdf-91b7-b6de11a02b95", "Student", "STUDENT" },
                    { "46e00514-0e07-41da-a30f-225a262c97a7", "f53ceaeb-a692-4b7a-ab4d-32c0a8a60efa", "Admin", "ADMIN" },
                    { "87dcc9dd-ddb8-4d05-83bc-82cfe62b019f", "63f46139-939b-49d6-a159-5594d14a67cf", "Interviewer", "INTERVIEWER" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "College", "FirstDepartment", "Grade", "Introduction", "Name", "Phone", "Qq", "SecondDepartment", "Skills", "State", "StudentId", "WeChat" },
                values: new object[,]
                {
                    { "07adf955-4631-4657-8d16-68b962274ef0", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "0942e729-6c53-42ed-87ba-308974514e47", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "0b4aee17-393d-4756-af33-46c14e8530c0", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "0bf4ca9f-a09c-405a-9149-99c76db9d3e2", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "0f692f32-3e52-431e-a448-923649648ca8", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "1482e87e-667c-41ce-b020-16137d078ab0", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "15433eb6-a290-4053-9559-de4f7567e8f8", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "16226bd7-c664-493b-a8af-9f4db1eac528", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "16d1ab7e-1447-47ef-a993-bf101de80793", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "19e996e6-0425-4d14-96a1-fe522505c34c", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "1af7effb-1d8e-4e45-b67a-bf3782ed82d9", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "1fc6b8c8-fc68-4add-b36b-ac2b0ecc1ec2", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "223a65a7-ff08-4ad7-910a-1f5b555ced3c", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "22b8334f-1e98-4597-8f9f-1ac71c081735", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "233d9d24-9de4-47da-a5a7-f119c0bd7906", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "259c8ed6-283f-4f9f-a802-cb847cc7d389", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "27307c20-a25e-44c5-be24-3b6e4c4085d8", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "2f199706-280b-465a-88a8-061b404d5669", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "302dd18c-8bc1-4418-864c-48f52874a1c6", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "32836baf-0bc0-4a33-9924-d2bf28d57151", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "3471166f-f296-4ef5-a1c8-ec1bc14c73ba", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "353455c1-4fda-4090-b266-cc3905ff6c35", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "3720bdc2-1b34-4612-af08-c8913679ad46", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "396a3229-3f33-4785-b91a-a8551b64ebfc", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "39f02c2c-abfe-4a42-89d2-2fd582c41ecf", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "3a692f54-4943-44b2-b420-1cc736dcd642", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "3da0252a-6b80-4453-b17e-ebcfe6d49031", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "416e4f88-5ef7-4668-853c-1c4b584c68a0", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "44e8c7fc-04ed-4e98-90d1-e6b869123485", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "460a9475-e473-46cc-a32e-26deada50ad3", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "49c4240d-3b52-4d9c-9d8f-12290aa5491e", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "4a96c055-a59e-4896-8eaa-59235d8f29ad", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "4ba612ce-37ff-4980-9294-da80f205fdac", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "4e026a46-fa66-4c68-b9d2-40826930170c", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "511b36f5-01da-4e06-97d2-5bf99d6f4fb8", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "53944bed-13d7-477d-b39c-f3518867026e", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "5a22fbf0-8949-4cba-8242-1794013a48d1", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "5b5dc131-7668-4266-816d-0f1aa2dbf0a9", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "5d2c77b3-1e91-4687-9fae-8fd6bb4cd0b5", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "5fd18f66-2751-4ccf-90c9-72175bbdc41a", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "61f57a02-f465-4f1f-8ce6-8d3c54804c7b", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "630eba5e-39ad-4a98-9407-f9b9d626ce35", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "64fd8d2e-b750-442f-b817-887b6b34cc16", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "654e0f39-8ee0-4af4-b7ff-80c413b81ceb", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "667efea3-4429-488b-8f90-a9e5dec47ed9", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "68aff82f-45b6-4d03-aa4e-b442884ce08b", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "68ddd41e-0b37-483a-ac01-637d9ad31184", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "69b2ace4-5958-4462-bd42-8269db67c0f1", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "6bd0d6b8-10ff-4f2e-9964-8906c6c086e6", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "6bd88aee-0c32-4e41-b47e-3a54d584faa7", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "6cb1e10e-3260-4392-af02-f9b188bb1ee9", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "7262b128-640d-4fe9-a2a1-aba76614b54a", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "72f8570f-4a39-4511-b703-604bbac6a8f6", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "7374220c-5109-49e2-bfa1-0c4cdb6960e7", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "74bc7e0f-747f-4a43-81ed-dd82d6ea1fd6", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "75fc3897-0dd8-4f96-9f18-830183c0dbeb", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "768bfd56-3f62-4b38-8608-4d947bd955db", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "83e882b1-e56a-4274-8092-e98173fa8f37", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "842abba3-a355-49c9-b39c-b038d73683b3", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "89dbddcb-c436-4dae-a23c-d192b8e7d8e8", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "8a1a5631-69af-4d33-8392-5dca1817de0d", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "8bf4391b-6e72-4a43-955e-cc965727660d", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "8bf779ea-1d6a-4ddb-a9e2-ea4d55b66d88", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "8da8bb4e-cee6-4abc-a65f-bdc51bf99b07", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "8e8e8907-609a-49cd-bb86-4a199c237a6e", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "9139f210-c52d-45b7-b751-7d0b07121b26", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "98f52159-2a41-4372-b7cd-003b464ebf97", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "996e30f1-897a-41c1-8613-98484210bd2c", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "aa085920-f419-412a-a426-9c7ef3e27a2b", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "ac74cb67-de19-47e4-9660-08e70e7219fc", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "ad2516e4-811d-49ab-88a6-b7d172c8118a", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "b2786cc4-3d49-454c-84f8-4e7e40e7f80e", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "b6d26b67-2a76-477b-b7d4-81f60d0ede73", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "b7b92175-c0e4-4da3-8a8e-aeb7764d324d", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "b7bcbe22-adb1-4f36-8db7-f20fbbf34cc8", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "b8368f83-eed7-48ca-bb00-eeb84919fd3f", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "b9dd7d49-d326-4688-bf9b-969cab3ed630", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "bab978e9-0bba-443c-b82e-bfe37439ace3", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "bb189769-1aad-4b49-9e16-5348a188a8a1", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "bf51feed-650c-46df-832f-39127eb27075", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c071cee5-5b80-4876-a3d0-6605e1090382", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c1fec9e3-4f28-4d33-be4a-f62a9eec68c3", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c48d73b4-9972-43ec-bca5-b257aa22e098", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c5982ace-28a5-45dd-8df6-cc49706af664", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c61adfea-099a-40ac-8856-d7956b27fb4c", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c9a0531c-60e9-48c3-bb10-90ef2d1ec45f", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "c9f667ec-5ede-4462-903f-1a63c28a4ad0", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "ca17f5b5-4cdc-430c-8ec1-dac60ea908ac", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "cb6c5ca1-acdd-4414-9332-67916aa6940d", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "cc5de8ed-300c-4a26-bd0e-0e45b81ef0b1", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "d1c243a4-83f8-4152-9075-6f809b2a0580", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "d3b9836d-59af-4ec5-b7bf-328b79229d5d", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "dbaeeeef-7f2b-494d-a6db-24589ca032ab", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "dc263940-1ac8-45f4-b6ba-aa35b946caf1", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "e701e1ff-5cda-4b49-9aa6-ff079ccc4b6b", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "e7313c2b-195a-4c17-bdcc-fe4a4609ed41", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "e7a6a668-4f2d-44a4-b807-aaf9d49983a4", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "e99fd37c-a5c8-4cd6-acd2-e8cd109f5173", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "ee87dffa-aa4f-4f48-a152-6fe845455f64", "计算机学院", 3, "22网络工程", "学生", "ChuHe", "13323588435", "235247902", null, null, 10, "3122004832", null },
                    { "fdadd46b-15c2-4f26-92e9-a8c70df728dd", "计算机学院", 7, "22网络工程", "学生", "HeChu", "13323588435", "235247902", null, null, 10, "3122004832", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Interviewers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
