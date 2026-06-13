using System.ComponentModel.DataAnnotations;

namespace FirstWeb.Models
{
    public class BusRoute
    {
        [Key]
        public int RouteId { get; set; }

        [Required]
        [StringLength(100)]
        public string RouteName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string RouteCode { get; set; } = string.Empty; // e.g., "RED", "BLUE", "ORANGE", "GREEN"

        [Required]
        [StringLength(50)]
        public string BusColor { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string FromStation { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string ToStation { get; set; } = string.Empty;

        [Required]
        public decimal Fare { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(100)]
        public string Frequency { get; set; } = string.Empty; // e.g., "Every 10 minutes"

        [StringLength(100)]
        public string OperatingHours { get; set; } = string.Empty; // e.g., "6:00 AM - 10:00 PM"

        public int TotalStops { get; set; }

        public double DistanceKm { get; set; }

        public int EstimatedTimeMinutes { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<BusStop> Stops { get; set; } = new List<BusStop>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
