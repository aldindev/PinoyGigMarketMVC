using PinoyGigMarket.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PinoyGigMarket.Models
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Location { get; set; }
        public string? GigPostPicturePath { get; set; }
        public ProjectStatus Status { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Budget { get; set; }


    }
}
