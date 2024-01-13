using Core.DataAccess;
using Core.Utilities;
using Entity;
using Entity.DTOs;

namespace DataAccess.Abstarct
{
    public interface IMessageDal : IEntityRepository<Message>
    {
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int tenantId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int tenantId, int messageId);
    }
}
