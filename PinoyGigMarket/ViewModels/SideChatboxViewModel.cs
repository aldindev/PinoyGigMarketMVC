namespace PinoyGigMarket.ViewModels
{
    public class SideChatboxViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageDate { get; set; }
        public bool IsUnread { get; set; }
    }
}
