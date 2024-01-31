using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace Bussiness.Abstract
{
    public interface IChatRoomService
    {
        DataResult<ChatRoom> Add(ChatRoomDTO chatRoomDTO);
        Result Delete(ChatRoom chatRoom);
        Result UpdateWithMessageDTO(MessageDTO messageDTO);

        Result DeleteById(int id);
        DataResult<ChatRoom> GetById(int chatRoomId);
        DataResult<ChatRoom> GetByTenantId(int tenantId);

        DataResult<List<ChatRoomVM>> GetChatRoomVMs(int apartmentComplexID);

    }
}
