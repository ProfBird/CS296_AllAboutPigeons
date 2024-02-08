using AllAboutPigeons.Models;

namespace AllAboutPigeons.Data
{
    public interface IMessageRepository
    {
        public List<Message> GetMessages();
        public Task<Message> GetMessageByIdAsync(int id);
        public Task<int> StoreMessageAsync(Message message);
    }
}
