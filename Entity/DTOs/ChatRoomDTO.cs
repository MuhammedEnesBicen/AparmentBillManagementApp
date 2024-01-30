using Entity.enums;

namespace Entity.DTOs
{
    public class ChatRoomDTO
    {
        public UserType User { get; set; }
        public int? LastSeenMessageId { get; set; }
        public int TenantId { get; set; }
    }
}
