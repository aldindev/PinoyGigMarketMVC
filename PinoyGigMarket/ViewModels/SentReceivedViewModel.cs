using PinoyGigMarket.Models;

namespace PinoyGigMarket.ViewModels
{
    public class SentReceivedViewModel
    {
        public IEnumerable<Message> SentMessages { get; set; }
        public IEnumerable<Message> ReceivedMessages { get; set; }
    }
}
