//structure for the vehciles database.
using System.ComponentModel.DataAnnotations;

namespace CarModificationCenter.Models
{
    public class website_vehicles
    {
        [Key] public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Engine { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string BodyType { get; set; }

        [Required]
        public string ExteriorColor { get; set; }

        [Required]
        public int DoorCount { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c0}")]
        public int Price { get; set; }

        [Required]
        public string Timestamp { get; set; }

        public byte[] Picture01 { get; set; }

        public string Description { get; set; }
    }
}