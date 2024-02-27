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

        // if there is a reply it will use this property
        public Message? Reply { get; set; } = null;

        // if this is a reply it will use this property for the ID of the original message
        public int? originalMessage { get; set; } = null;
    }

}
