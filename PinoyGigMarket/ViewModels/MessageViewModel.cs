namespace PinoyGigMarket.ViewModels
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public string? SenderId { get; set; }
        public string? SenderFirstName { get; set; }
        public string? SenderLastName { get; set; }
        public string ReceiverId { get; set; }
        public string? ReceiverFirstName { get; set; }
        public string? ReceiverLastName { get; set; }
        public DateTime DateSent { get; set; }
        public bool? IsRead { get; set; }
        public bool IsSent { get; set; } // Indicates if the message was sent by the logged-in user
    }
}
