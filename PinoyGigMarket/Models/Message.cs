using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PinoyGigMarket.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [Required]
        [ForeignKey("Recipient")]
        public string ReceiverId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;

        // Navigation properties
        public virtual AppUser Sender { get; set; }
        public virtual AppUser Receiver { get; set; }
    }
}
