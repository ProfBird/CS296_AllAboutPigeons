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
           var message = await _context.Messages.FindAsync(id);
            _context.Entry(message).Reference(m => m.To).Load(); 
            _context.Entry(message).Reference(m => m.From).Load();
            _context.Entry(message).Collection(m => m.Replies).Load();
            return message;
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
            return await _context.SaveChangesAsync();
        }

        // Update the database with any changes made to the message object.
        public int UpdateMessage(Message message)
        {
            _context.Update(message);
            // Returns the number of updated saved
            return _context.SaveChanges();
        }

        public int DeleteMessage(int messageId)
        {
            Message message = GetMessageByIdAsync(messageId).Result;
            // If the message has replies, remove them first to avoid a FK constraint violation
            if (message.Replies.Count > 0)
            {
                foreach (var reply in message.Replies)
                {
                    _context.Messages.Remove(reply);
                }
            }
            _context.Messages.Remove(message);
            return _context.SaveChanges();
        }
    }
}
