using Entity.enums;

namespace Entity.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime MessageTime { get; set; }
        public UserType Sender { get; set; }
        public int ChatRoomId { get; set; }
    }
}
