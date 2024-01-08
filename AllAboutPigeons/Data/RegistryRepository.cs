using AllAboutPigeons.Models;
using Microsoft.EntityFrameworkCore;

namespace AllAboutPigeons.Data
{
    public class RegistryRepository : IRegistryRepository
    {
        AppDbContext context;
        public RegistryRepository(AppDbContext c) 
        {
            context = c;
        }

        public Message GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessages()
        {
            return context.Messages
            .Include(m => m.To)
            .Include(m => m.From)
            .ToList();
        }

        public int StoreMessage(Message message)
        {
            context.Add(message);
            // Returns the number of objects saved
            return context.SaveChanges();
        }
    }
}
