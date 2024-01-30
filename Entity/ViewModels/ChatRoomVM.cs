namespace Entity.ViewModels
{
    public class ChatRoomVM
    {
        public int ChatRoomId { get; set; }
        public int? LastSeenMessageId { get; set; }
        public string? LastSeenMessage { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantLastName { get; set; }

        public int ApartmentId { get; set; }
        public string BlockName { get; set; }
        public int FlatNumber { get; set; }

        public DateTime? LastChatTime { get; set; }
        public int UnreadMessageCount { get; set; }

    }
}
