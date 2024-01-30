using Entity.enums;

namespace Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime MessageTime { get; set; }
        public UserType Sender { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

    }
}
