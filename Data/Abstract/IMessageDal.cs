using Core.DataAccess;
using Core.Utilities;
using Entity;
using Entity.DTOs;

namespace DataAccess.Abstract
{
    public interface IMessageDal : IEntityRepository<Message>
    {
        new public DataResult<Message> Add(Message message);
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int chatRoomId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int chatRoomId);

        int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId);
        int GetUnreadMessageCountByApartmentComplexId(int apartmentComplexId);
    }
}
