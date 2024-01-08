using AllAboutPigeons.Models;

namespace AllAboutPigeons.Data
{
    public class FakeRegistryRepository : IRegistryRepository
    {
        List<Message> messages = new List<Message>();


        public Message GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessages()
        {
            throw new NotImplementedException();
        }

        public int StoreMessage(Message message)
        {
            message.MessageId = messages.Count + 1; // Temp
            messages.Add(message);
            return message.MessageId;
        }
    }
}
