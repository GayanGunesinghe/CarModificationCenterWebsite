using System.ComponentModel.DataAnnotations;

namespace CarModificationCenter.Models
{
    public class project
    {
        [Required]
        public string ProjectID { get; set; }

        [Required]
        public string CustomerID { get; set; }

        public string ProjectName { get; set; }

        public string Type { get; set; }

        public string Design { get; set; }

        public string Status { get; set; }

        public string AcceptedDate { get; set; }

        public string Deadline { get; set; }

        public byte[] MainImage { get; set; }
    }
}