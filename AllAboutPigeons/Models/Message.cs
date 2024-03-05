using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutPigeons.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public AppUser To {  get; set; }
        public AppUser From { get; set; }
        public string Text {  get; set; }
        public DateOnly Date {  get; set; }
        public int Rating { get; set; }
        
        [ForeignKey("OriginalMessageId")]
        public List<Message> Replies { get; set; } = new List<Message>();
        public int? OriginalMessageId { get; set; } = null;
    }

}
