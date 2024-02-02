using AllAboutPigeons.Models;

namespace AllAboutPigeons.Data
{
    public class FakeMessageRepository : IMessageRepository
    {
        List<Message> _messages = new List<Message>();

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
            message.MessageId = _messages.Count + 1; // Temp
            _messages.Add(message);
            return message.MessageId;
        }
    }
}
