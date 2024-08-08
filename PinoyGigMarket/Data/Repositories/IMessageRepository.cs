using PinoyGigMarket.ViewModels;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Data.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetUserMessages(string userId);
        Task<Message> GetMessageById(int id);
        Task SendMessage(Message message);
    }

}
