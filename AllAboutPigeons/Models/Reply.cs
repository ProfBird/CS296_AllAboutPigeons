using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutPigeons.Models
{
    public class Reply : Message
    {
        // This class inherits MessageId and it will be a FK here

        // if this is a reply this property points back to the original message
        public int? idOriginalMessage { get; set; } = null;
    }
}
