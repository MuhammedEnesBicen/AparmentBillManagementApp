using Core.Utilities;
using Entity.DTOs;
using Entity.enums;

namespace Bussiness.Abstract
{
    public interface IMessageService
    {
        public DataResult<MessageDTO> Add(MessageDTO messageDTO);
        public Result DeleteById(int messageId);
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int chatRoomId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int chatRoomId);

        int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId);
    }
}
