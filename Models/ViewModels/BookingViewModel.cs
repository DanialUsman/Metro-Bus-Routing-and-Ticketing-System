using System.ComponentModel.DataAnnotations;

namespace FirstWeb.Models.ViewModels
{
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string PassengerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string PassengerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your phone number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PassengerPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a route")]
        [Display(Name = "Bus Route")]
        public int RouteId { get; set; }

        [Required(ErrorMessage = "Please select a boarding stop")]
        [Display(Name = "Boarding Stop")]
        public string BoardingStop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a destination stop")]
        [Display(Name = "Destination Stop")]
        public string DestinationStop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a travel date")]
        [DataType(DataType.Date)]
        [Display(Name = "Travel Date")]
        public DateTime TravelDate { get; set; } = DateTime.Today.AddDays(1);

        [Required]
        [Range(1, 10, ErrorMessage = "Number of passengers must be between 1 and 10")]
        [Display(Name = "Number of Passengers")]
        public int NumberOfPassengers { get; set; } = 1;

        // For populating dropdowns
        public List<BusRoute>? AvailableRoutes { get; set; }
        public List<BusStop>? AvailableStops { get; set; }
        public decimal? FarePerPerson { get; set; }
    }

    public class RouteDetailsViewModel
    {
        public BusRoute Route { get; set; } = null!;
        public List<BusStop> Stops { get; set; } = new();
    }

    public class BookingConfirmationViewModel
    {
        public Booking Booking { get; set; } = null!;
        public BusRoute Route { get; set; } = null!;
    }
}
