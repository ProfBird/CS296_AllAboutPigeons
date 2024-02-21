using AllAboutPigeons.Models;
using Microsoft.EntityFrameworkCore;

namespace AllAboutPigeons.Data
{
    public class MessageRepository : IMessageRepository
    {
        AppDbContext _context;
        public MessageRepository(AppDbContext c) 
        {
            _context = c;
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public List<Message> GetMessages()
        {
            return _context.Messages
            .Include(m => m.To)
            .Include(m => m.From)
            .ToList<Message>();
        }

        public async Task<int> StoreMessageAsync(Message message)
        {
            await _context.AddAsync(message);
            // Returns the number of objects saved
            return _context.SaveChanges();
        }

        public int DeleteMessage(int messageId)
        {
            Message message = _context.Messages.Find(messageId);
            _context.Remove(message); 
            return _context.SaveChanges();
        }
    }
}
