using Core.DataAccess;
using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace DataAccess.Abstarct
{
    public interface IChatRoomDal : IEntityRepository<ChatRoom>
    {
        new public DataResult<ChatRoom> Add(ChatRoom chatRoom);
        public DataResult<List<ChatRoomVM>> GetChatRoomVMs(int apartmentComplexId);
    }
}
