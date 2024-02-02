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

        public Message GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessages()
        {
            return _context.Messages
            .Include(m => m.To)
            .Include(m => m.From)
            .ToList();
        }

        public int StoreMessage(Message message)
        {
            _context.Add(message);
            // Returns the number of objects saved
            return _context.SaveChanges();
        }
    }
}
