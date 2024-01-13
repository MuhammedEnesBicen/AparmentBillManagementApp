using Core.Utilities;
using Entity;
using Entity.DTOs;

namespace Bussiness.Abstract
{
    public interface IMessageService
    {
        public Result Add(MessageDTO messageDTO);
        public Result DeleteById(int messageId);
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int tenantId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int tenantId, int messageId);
    }
}
