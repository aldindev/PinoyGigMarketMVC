using System.ComponentModel.DataAnnotations;

namespace PinoyGigMarket.Models
{
    public class SkillViewModel
    {
        public int SkillId { get; set; }

        [Required]
        public string SkillName { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public string Rate { get; set; }
    }
}
