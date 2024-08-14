using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Models;
using PinoyGigMarket.ViewModels;
using System.Collections.Generic;

namespace PinoyGigMarket.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetUserMessages(string userId, bool isSent)
        {
            IQueryable<Message> query;

            if (isSent)
            {
                query = _context.Messages
                    .Where(m => m.SenderId == userId)
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver);
            }
            else
            {
                query = _context.Messages
                    .Where(m => m.ReceiverId == userId)
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver);
            }

            return await query.OrderByDescending(m => m.SentAt).ToListAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .FirstOrDefaultAsync(m => m.MessageId == id);
        }

        public async Task SendMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

    }
}
