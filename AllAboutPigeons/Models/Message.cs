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
        
        // Use this property if there is a reply to this message
        public Message Reply { get; set; } = null!;
        
        // Use this property for replies. If it has a value, it will be cascade deleted.
        public int? OriginalMessageId { get; set; } = null!;
    }
    
}
