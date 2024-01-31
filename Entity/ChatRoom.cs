using Entity.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class ChatRoom
    {
        public int Id { get; set; }

        [ForeignKey("Message")]
        public int? LastSeenMessageId { get; set; }
        public Message? Message { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

    }
}
