namespace Entity.ViewModels
{
    public class ChatRoomVM
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantLastName { get; set; }

        public int ApartmentId { get; set; }
        public string BlockName { get; set; }
        public int FlatNumber { get; set; }

        public DateTime LastChatTime { get; set; }

    }
}
