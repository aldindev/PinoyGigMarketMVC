using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinoyGigMarket.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public string SkillName { get; set; }
        public string Desc { get; set; }
        public string Rate { get; set; }

        // Navigation property
        public virtual AppUser User { get; set; }

    }
}
