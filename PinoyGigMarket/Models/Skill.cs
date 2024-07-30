using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinoyGigMarket.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        [Required]
        public string SkillName { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public string Rate { get; set; }

        // Navigation property
        public virtual AppUser User { get; set; }
    }
}
