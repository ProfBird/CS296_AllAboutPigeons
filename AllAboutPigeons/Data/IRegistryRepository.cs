using AllAboutPigeons.Models;

namespace AllAboutPigeons.Data
{
    public interface IRegistryRepository
    {
        public List<Message> GetMessages();
        public Message GetMessageById(int id);
        public int StoreMessage(Message message);
    }
}
