using PinoyGigMarket.Models;

namespace PinoyGigMarket.Data.Repositories
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<Message>> GetUserMessages(string userId)
        {
            return await _messageRepository.GetUserMessages(userId);
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _messageRepository.GetMessageById(id);
        }

        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            await _messageRepository.SendMessage(message);
        }
    }
}
