using Core.Utilities;
using Entity.DTOs;

namespace Bussiness.Abstract
{
    public interface IMessageService
    {
        public DataResult<MessageDTO> Add(MessageDTO messageDTO);
        public Result DeleteById(int messageId);
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int chatRoomId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int chatRoomId);
        public DataResult<List<MessageDTO>> GetNewMessagesByChatRoomIdAndMessageId(int chatRoomId, int messageId);

        int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId);
    }
}
