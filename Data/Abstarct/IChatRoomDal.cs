using Core.DataAccess;
using Core.Utilities;
using Entity;
using Entity.ViewModels;

namespace DataAccess.Abstarct
{
    public interface IChatRoomDal : IEntityRepository<ChatRoom>
    {
        new public DataResult<ChatRoom> Add(ChatRoom chatRoom);
        public DataResult<List<ChatRoomVM>> GetChatRoomVMs(int apartmentComplexId);
        public Result UpdateLastSeenMessageIdWithNewMax(int chatRoomId, int messageId);
    }
}
