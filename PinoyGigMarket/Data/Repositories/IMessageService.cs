using PinoyGigMarket.Models;

namespace PinoyGigMarket.Data.Repositories
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetUserMessages(string userId);
        Task<Message> GetMessageById(int id);
        Task SendMessage(string senderId, string receiverId, string content);
    }
}
