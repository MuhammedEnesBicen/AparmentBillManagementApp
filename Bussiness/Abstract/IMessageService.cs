using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace Bussiness.Abstract
{
    public interface IMessageService
    {
        public DataResult<MessageDTO> Add(MessageDTO messageDTO);
        public Result DeleteById(int messageId);
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int tenantId);
        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int tenantId, int messageId);
        public DataResult<List<ChatRoomVM>> GetChatRooms(int apartmentComplexId);
    }
}
