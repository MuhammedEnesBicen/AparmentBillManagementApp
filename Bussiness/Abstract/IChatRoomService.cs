using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.enums;
using Entity.ViewModels;

namespace Bussiness.Abstract
{
    public interface IChatRoomService
    {
        DataResult<ChatRoom> Add(ChatRoomDTO chatRoomDTO);
        Result Delete(ChatRoom chatRoom);
        Result UpdateWithMessageDTO(MessageDTO messageDTO, UserType forWhichUser);

        Result DeleteById(int id);
        DataResult<ChatRoom> GetById(int chatRoomId);
        DataResult<ChatRoom> GetByTenantIdAndUserType(int tenantId, UserType userType);

        DataResult<List<ChatRoomVM>> GetChatRoomVMs(int apartmentComplexID);

    }
}
