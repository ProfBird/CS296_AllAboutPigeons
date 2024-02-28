using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutPigeons.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public AppUser To {  get; set; }
        public AppUser From { get; set; }
        public string Text {  get; set; }
        public DateOnly Date {  get; set; }
        public int Rating { get; set; }

        // if there are replies this property will reference them
        public List<Reply> Replies { get; set; } = new List<Reply>();
        public int? idOriginalMessage { get; set; } = null;
    }

}
