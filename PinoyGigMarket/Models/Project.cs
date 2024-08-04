using PinoyGigMarket.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinoyGigMarket.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [ForeignKey("Client")]
        public string ClientID { get; set; }
        public string FreelancerID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public ProjectStatus Status { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Budget { get; set; }

        public string Location { get; set; }
        public string GigPostPicturePath { get; set; } = "/images/default/defaultgigpost.png";

        //Navigation
        public virtual AppUser Client { get; set; }
        public virtual AppUser Freelancer { get; set; }

    }
}
