using FirstWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Data
{
    public class MetroBusContext : DbContext
    {
        public MetroBusContext(DbContextOptions<MetroBusContext> options)
                   : base(options)
        {
        }

        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusRoute>().Property(r => r.Fare).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Booking>().Property(b => b.TotalFare).HasColumnType("decimal(10,2)");

            // Seed Bus Routes - Real Rawalpindi/Islamabad Metro Routes
            modelBuilder.Entity<BusRoute>().HasData(
                new BusRoute
                {
                    RouteId = 1,
                    RouteName = "Red Line - Rawalpindi Metro",
                    RouteCode = "RED",
                    BusColor = "#DC2626",
                    FromStation = "Saddar",
                    ToStation = "Islamabad Secretariat",
                    Fare = 50,
                    Description = "The Red Line connects Saddar Rawalpindi to Islamabad Secretariat, passing through major commercial areas and government offices. Air-conditioned buses with comfortable seating.",
                    Frequency = "Every 10 minutes",
                    OperatingHours = "6:00 AM - 10:00 PM",
                    TotalStops = 12,
                    DistanceKm = 22.5,
                    EstimatedTimeMinutes = 45,
                    IsActive = true
                },
                new BusRoute
                {
                    RouteId = 2,
                    RouteName = "Blue Line - Islamabad Express",
                    RouteCode = "BLUE",
                    BusColor = "#2563EB",
                    FromStation = "Faizabad",
                    ToStation = "Faisal Mosque",
                    Fare = 50,
                    Description = "The Blue Line runs through the heart of Islamabad, connecting Faizabad to Faisal Mosque via major landmarks including Jinnah Avenue and the Blue Area commercial district.",
                    Frequency = "Every 12 minutes",
                    OperatingHours = "6:00 AM - 10:30 PM",
                    TotalStops = 10,
                    DistanceKm = 18.0,
                    EstimatedTimeMinutes = 35,
                    IsActive = true
                },
                new BusRoute
                {
                    RouteId = 3,
                    RouteName = "Orange Line - Airport Connector",
                    RouteCode = "ORANGE",
                    BusColor = "#EA580C",
                    FromStation = "Rawalpindi Railway Station",
                    ToStation = "Islamabad International Airport",
                    Fare = 80,
                    Description = "The Orange Line provides direct connectivity from Rawalpindi Railway Station to the New Islamabad International Airport, serving commuters and travelers with premium service.",
                    Frequency = "Every 15 minutes",
                    OperatingHours = "5:00 AM - 11:00 PM",
                    TotalStops = 14,
                    DistanceKm = 35.0,
                    EstimatedTimeMinutes = 55,
                    IsActive = true
                },
                new BusRoute
                {
                    RouteId = 4,
                    RouteName = "Green Line - Margalla Express",
                    RouteCode = "GREEN",
                    BusColor = "#16A34A",
                    FromStation = "Pirwadhai",
                    ToStation = "Centaurus Mall",
                    Fare = 40,
                    Description = "The Green Line serves the scenic route along Margalla Hills foothill, connecting Pirwadhai bus terminal to Centaurus Mall, passing through residential and commercial zones of Islamabad.",
                    Frequency = "Every 8 minutes",
                    OperatingHours = "6:00 AM - 10:00 PM",
                    TotalStops = 11,
                    DistanceKm = 16.0,
                    EstimatedTimeMinutes = 30,
                    IsActive = true
                }
            );

            // Seed Bus Stops - Real locations in Rawalpindi/Islamabad
            modelBuilder.Entity<BusStop>().HasData(
                // RED LINE Stops
                new BusStop { StopId = 1, StopName = "Saddar", StopOrder = 1, Area = "Saddar, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 2, StopName = "Committee Chowk", StopOrder = 2, Area = "Committee Chowk, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 3, StopName = "Liaquat Bagh", StopOrder = 3, Area = "Liaquat Bagh, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 4, StopName = "Marrir Chowk", StopOrder = 4, Area = "Marrir Chowk, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 5, StopName = "6th Road", StopOrder = 5, Area = "Satellite Town, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 6, StopName = "Rehmanabad", StopOrder = 6, Area = "Rehmanabad, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 7, StopName = "Chandni Chowk", StopOrder = 7, Area = "Chandni Chowk, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 8, StopName = "Faizabad", StopOrder = 8, Area = "Faizabad, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 9, StopName = "IJP Road", StopOrder = 9, Area = "IJP Road, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 10, StopName = "Potohar Town", StopOrder = 10, Area = "Potohar Town, Rawalpindi", RouteId = 1 },
                new BusStop { StopId = 11, StopName = "Pakistan Secretariat", StopOrder = 11, Area = "Secretariat, Islamabad", RouteId = 1 },
                new BusStop { StopId = 12, StopName = "Islamabad Secretariat", StopOrder = 12, Area = "Secretariat, Islamabad", RouteId = 1 },

                // BLUE LINE Stops
                new BusStop { StopId = 13, StopName = "Faizabad", StopOrder = 1, Area = "Faizabad, Rawalpindi", RouteId = 2 },
                new BusStop { StopId = 14, StopName = "Shamsabad", StopOrder = 2, Area = "Shamsabad, Rawalpindi", RouteId = 2 },
                new BusStop { StopId = 15, StopName = "I-8 Markaz", StopOrder = 3, Area = "I-8, Islamabad", RouteId = 2 },
                new BusStop { StopId = 16, StopName = "Aabpara", StopOrder = 4, Area = "Aabpara, Islamabad", RouteId = 2 },
                new BusStop { StopId = 17, StopName = "Blue Area (Jinnah Ave)", StopOrder = 5, Area = "Blue Area, Islamabad", RouteId = 2 },
                new BusStop { StopId = 18, StopName = "Melody Market", StopOrder = 6, Area = "G-6, Islamabad", RouteId = 2 },
                new BusStop { StopId = 19, StopName = "Super Market (F-6)", StopOrder = 7, Area = "F-6, Islamabad", RouteId = 2 },
                new BusStop { StopId = 20, StopName = "F-7 Markaz", StopOrder = 8, Area = "F-7, Islamabad", RouteId = 2 },
                new BusStop { StopId = 21, StopName = "E-7 Sector", StopOrder = 9, Area = "E-7, Islamabad", RouteId = 2 },
                new BusStop { StopId = 22, StopName = "Faisal Mosque", StopOrder = 10, Area = "E-7, Islamabad", RouteId = 2 },

                // ORANGE LINE Stops
                new BusStop { StopId = 23, StopName = "Rawalpindi Railway Station", StopOrder = 1, Area = "Saddar, Rawalpindi", RouteId = 3 },
                new BusStop { StopId = 24, StopName = "Commercial Market", StopOrder = 2, Area = "Satellite Town, Rawalpindi", RouteId = 3 },
                new BusStop { StopId = 25, StopName = "Shamsabad", StopOrder = 3, Area = "Shamsabad, Rawalpindi", RouteId = 3 },
                new BusStop { StopId = 26, StopName = "Faizabad", StopOrder = 4, Area = "Faizabad, Rawalpindi", RouteId = 3 },
                new BusStop { StopId = 27, StopName = "Pirwadhai Mor", StopOrder = 5, Area = "Pirwadhai, Rawalpindi", RouteId = 3 },
                new BusStop { StopId = 28, StopName = "I-9 Industrial Area", StopOrder = 6, Area = "I-9, Islamabad", RouteId = 3 },
                new BusStop { StopId = 29, StopName = "I-10 Markaz", StopOrder = 7, Area = "I-10, Islamabad", RouteId = 3 },
                new BusStop { StopId = 30, StopName = "I-11 Sector", StopOrder = 8, Area = "I-11, Islamabad", RouteId = 3 },
                new BusStop { StopId = 31, StopName = "I-14 Sector", StopOrder = 9, Area = "I-14, Islamabad", RouteId = 3 },
                new BusStop { StopId = 32, StopName = "I-16 Sector", StopOrder = 10, Area = "I-16, Islamabad", RouteId = 3 },
                new BusStop { StopId = 33, StopName = "Taxila Bypass", StopOrder = 11, Area = "Taxila, Rawalpindi", RouteId = 3 },
                new BusStop { StopId = 34, StopName = "Tarnol", StopOrder = 12, Area = "Tarnol, Islamabad", RouteId = 3 },
                new BusStop { StopId = 35, StopName = "Golra Mor", StopOrder = 13, Area = "Golra, Islamabad", RouteId = 3 },
                new BusStop { StopId = 36, StopName = "Islamabad International Airport", StopOrder = 14, Area = "Airport, Islamabad", RouteId = 3 },

                // GREEN LINE Stops
                new BusStop { StopId = 37, StopName = "Pirwadhai", StopOrder = 1, Area = "Pirwadhai, Rawalpindi", RouteId = 4 },
                new BusStop { StopId = 38, StopName = "6th Road Chowk", StopOrder = 2, Area = "Satellite Town, Rawalpindi", RouteId = 4 },
                new BusStop { StopId = 39, StopName = "Murree Road", StopOrder = 3, Area = "Murree Road, Rawalpindi", RouteId = 4 },
                new BusStop { StopId = 40, StopName = "Faizabad", StopOrder = 4, Area = "Faizabad, Rawalpindi", RouteId = 4 },
                new BusStop { StopId = 41, StopName = "Pir Sohawa Road", StopOrder = 5, Area = "Margalla Hills, Islamabad", RouteId = 4 },
                new BusStop { StopId = 42, StopName = "F-10 Markaz", StopOrder = 6, Area = "F-10, Islamabad", RouteId = 4 },
                new BusStop { StopId = 43, StopName = "F-8 Markaz", StopOrder = 7, Area = "F-8, Islamabad", RouteId = 4 },
                new BusStop { StopId = 44, StopName = "F-7 Markaz", StopOrder = 8, Area = "F-7, Islamabad", RouteId = 4 },
                new BusStop { StopId = 45, StopName = "Jinnah Super Market", StopOrder = 9, Area = "F-7, Islamabad", RouteId = 4 },
                new BusStop { StopId = 46, StopName = "Blue Area", StopOrder = 10, Area = "Blue Area, Islamabad", RouteId = 4 },
                new BusStop { StopId = 47, StopName = "Centaurus Mall", StopOrder = 11, Area = "F-8, Islamabad", RouteId = 4 }
            );
        }
    }
}
