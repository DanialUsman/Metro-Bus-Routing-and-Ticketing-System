using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWeb.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [StringLength(450)]
        public string UserId { get; set; } = string.Empty; // Identity User ID

        [Required]
        [StringLength(100)]
        public string PassengerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string PassengerEmail { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string PassengerPhone { get; set; } = string.Empty;

        // Route info
        public int RouteId { get; set; }

        [ForeignKey("RouteId")]
        public BusRoute? Route { get; set; }

        [Required]
        [StringLength(200)]
        public string BoardingStop { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string DestinationStop { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime TravelDate { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumberOfPassengers { get; set; } = 1;

        [Required]
        public decimal TotalFare { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string BookingReference { get; set; } = string.Empty;
    }
}
