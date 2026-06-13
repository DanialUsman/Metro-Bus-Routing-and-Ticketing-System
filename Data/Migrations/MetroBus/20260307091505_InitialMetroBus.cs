using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstWeb.Data.Migrations.MetroBus
{
    /// <inheritdoc />
    public partial class InitialMetroBus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusRoutes",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RouteCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BusColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FromStation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ToStation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fare = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OperatingHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalStops = table.Column<int>(type: "int", nullable: false),
                    DistanceKm = table.Column<double>(type: "float", nullable: false),
                    EstimatedTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRoutes", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PassengerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PassengerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PassengerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    BoardingStop = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DestinationStop = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPassengers = table.Column<int>(type: "int", nullable: false),
                    TotalFare = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingReference = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_BusRoutes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "BusRoutes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    StopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StopName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StopOrder = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RouteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.StopId);
                    table.ForeignKey(
                        name: "FK_BusStops_BusRoutes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "BusRoutes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BusRoutes",
                columns: new[] { "RouteId", "BusColor", "Description", "DistanceKm", "EstimatedTimeMinutes", "Fare", "Frequency", "FromStation", "IsActive", "OperatingHours", "RouteCode", "RouteName", "ToStation", "TotalStops" },
                values: new object[,]
                {
                    { 1, "#DC2626", "The Red Line connects Saddar Rawalpindi to Islamabad Secretariat, passing through major commercial areas and government offices. Air-conditioned buses with comfortable seating.", 22.5, 45, 50m, "Every 10 minutes", "Saddar", true, "6:00 AM - 10:00 PM", "RED", "Red Line - Rawalpindi Metro", "Islamabad Secretariat", 12 },
                    { 2, "#2563EB", "The Blue Line runs through the heart of Islamabad, connecting Faizabad to Faisal Mosque via major landmarks including Jinnah Avenue and the Blue Area commercial district.", 18.0, 35, 50m, "Every 12 minutes", "Faizabad", true, "6:00 AM - 10:30 PM", "BLUE", "Blue Line - Islamabad Express", "Faisal Mosque", 10 },
                    { 3, "#EA580C", "The Orange Line provides direct connectivity from Rawalpindi Railway Station to the New Islamabad International Airport, serving commuters and travelers with premium service.", 35.0, 55, 80m, "Every 15 minutes", "Rawalpindi Railway Station", true, "5:00 AM - 11:00 PM", "ORANGE", "Orange Line - Airport Connector", "Islamabad International Airport", 14 },
                    { 4, "#16A34A", "The Green Line serves the scenic route along Margalla Hills foothill, connecting Pirwadhai bus terminal to Centaurus Mall, passing through residential and commercial zones of Islamabad.", 16.0, 30, 40m, "Every 8 minutes", "Pirwadhai", true, "6:00 AM - 10:00 PM", "GREEN", "Green Line - Margalla Express", "Centaurus Mall", 11 }
                });

            migrationBuilder.InsertData(
                table: "BusStops",
                columns: new[] { "StopId", "Area", "RouteId", "StopName", "StopOrder" },
                values: new object[,]
                {
                    { 1, "Saddar, Rawalpindi", 1, "Saddar", 1 },
                    { 2, "Committee Chowk, Rawalpindi", 1, "Committee Chowk", 2 },
                    { 3, "Liaquat Bagh, Rawalpindi", 1, "Liaquat Bagh", 3 },
                    { 4, "Marrir Chowk, Rawalpindi", 1, "Marrir Chowk", 4 },
                    { 5, "Satellite Town, Rawalpindi", 1, "6th Road", 5 },
                    { 6, "Rehmanabad, Rawalpindi", 1, "Rehmanabad", 6 },
                    { 7, "Chandni Chowk, Rawalpindi", 1, "Chandni Chowk", 7 },
                    { 8, "Faizabad, Rawalpindi", 1, "Faizabad", 8 },
                    { 9, "IJP Road, Rawalpindi", 1, "IJP Road", 9 },
                    { 10, "Potohar Town, Rawalpindi", 1, "Potohar Town", 10 },
                    { 11, "Secretariat, Islamabad", 1, "Pakistan Secretariat", 11 },
                    { 12, "Secretariat, Islamabad", 1, "Islamabad Secretariat", 12 },
                    { 13, "Faizabad, Rawalpindi", 2, "Faizabad", 1 },
                    { 14, "Shamsabad, Rawalpindi", 2, "Shamsabad", 2 },
                    { 15, "I-8, Islamabad", 2, "I-8 Markaz", 3 },
                    { 16, "Aabpara, Islamabad", 2, "Aabpara", 4 },
                    { 17, "Blue Area, Islamabad", 2, "Blue Area (Jinnah Ave)", 5 },
                    { 18, "G-6, Islamabad", 2, "Melody Market", 6 },
                    { 19, "F-6, Islamabad", 2, "Super Market (F-6)", 7 },
                    { 20, "F-7, Islamabad", 2, "F-7 Markaz", 8 },
                    { 21, "E-7, Islamabad", 2, "E-7 Sector", 9 },
                    { 22, "E-7, Islamabad", 2, "Faisal Mosque", 10 },
                    { 23, "Saddar, Rawalpindi", 3, "Rawalpindi Railway Station", 1 },
                    { 24, "Satellite Town, Rawalpindi", 3, "Commercial Market", 2 },
                    { 25, "Shamsabad, Rawalpindi", 3, "Shamsabad", 3 },
                    { 26, "Faizabad, Rawalpindi", 3, "Faizabad", 4 },
                    { 27, "Pirwadhai, Rawalpindi", 3, "Pirwadhai Mor", 5 },
                    { 28, "I-9, Islamabad", 3, "I-9 Industrial Area", 6 },
                    { 29, "I-10, Islamabad", 3, "I-10 Markaz", 7 },
                    { 30, "I-11, Islamabad", 3, "I-11 Sector", 8 },
                    { 31, "I-14, Islamabad", 3, "I-14 Sector", 9 },
                    { 32, "I-16, Islamabad", 3, "I-16 Sector", 10 },
                    { 33, "Taxila, Rawalpindi", 3, "Taxila Bypass", 11 },
                    { 34, "Tarnol, Islamabad", 3, "Tarnol", 12 },
                    { 35, "Golra, Islamabad", 3, "Golra Mor", 13 },
                    { 36, "Airport, Islamabad", 3, "Islamabad International Airport", 14 },
                    { 37, "Pirwadhai, Rawalpindi", 4, "Pirwadhai", 1 },
                    { 38, "Satellite Town, Rawalpindi", 4, "6th Road Chowk", 2 },
                    { 39, "Murree Road, Rawalpindi", 4, "Murree Road", 3 },
                    { 40, "Faizabad, Rawalpindi", 4, "Faizabad", 4 },
                    { 41, "Margalla Hills, Islamabad", 4, "Pir Sohawa Road", 5 },
                    { 42, "F-10, Islamabad", 4, "F-10 Markaz", 6 },
                    { 43, "F-8, Islamabad", 4, "F-8 Markaz", 7 },
                    { 44, "F-7, Islamabad", 4, "F-7 Markaz", 8 },
                    { 45, "F-7, Islamabad", 4, "Jinnah Super Market", 9 },
                    { 46, "Blue Area, Islamabad", 4, "Blue Area", 10 },
                    { 47, "F-8, Islamabad", 4, "Centaurus Mall", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RouteId",
                table: "Bookings",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_RouteId",
                table: "BusStops",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "BusRoutes");
        }
    }
}
