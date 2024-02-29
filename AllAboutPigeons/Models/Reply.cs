using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutPigeons.Models
{
    public class Reply : Message
    {
        public int ReplyId { get; set; }
        // This class inherits MessageId and it will be a FK for Reply objects
    }
}
