using Microsoft.AspNetCore.Authentication;
using PinoyGigMarket.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinoyGigMarket.Models
{
    public class Proposal
    {
        [Key]
        public int ProposalId { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [ForeignKey("Freelancer")]
        public string FreelancerID { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal BidAmount { get; set; }
        public ProjectStatus Status { get; set; }

        //Navigation
        public virtual Project Project { get; set; }
        public virtual AppUser Freelancer { get; set; }

    }
}
