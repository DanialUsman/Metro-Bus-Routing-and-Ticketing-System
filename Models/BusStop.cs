using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWeb.Models
{
    public class BusStop
    {
        [Key]
        public int StopId { get; set; }

        [Required]
        [StringLength(200)]
        public string StopName { get; set; } = string.Empty;

        public int StopOrder { get; set; } // Order of stop in the route

        [StringLength(300)]
        public string? Area { get; set; } // e.g., "Saddar, Rawalpindi"

        // Foreign key
        public int RouteId { get; set; }

        [ForeignKey("RouteId")]
        public BusRoute? Route { get; set; }
    }
}
