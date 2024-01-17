using Core.DataAccess;
using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace DataAccess.Abstarct
{
    public interface IMessageDal : IEntityRepository<Message>
    {
        new public DataResult<Message> Add(Message message);
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int tenantId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int tenantId, int messageId);
        public DataResult<List<ChatRoomVM>> GetChatRooms(int apartmentComplexId);

    }
}
