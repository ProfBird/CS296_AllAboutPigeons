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

        // if there is a reply this property will reference it
        public Message? Reply { get; set; } = null;

        // if this is a reply this property  to point back to the original message
        public int? idOriginalMessage { get; set; } = null;
    }

}
