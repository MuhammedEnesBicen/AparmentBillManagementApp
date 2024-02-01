using Core.DataAccess.EntityFramework;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.ViewModels;

namespace DataAccess.Concrete
{
    public class EfChatRoomDal : EfEntityRepositoryBase<ChatRoom, AppDbContext>, IChatRoomDal
    {
        DataResult<ChatRoom> IChatRoomDal.Add(ChatRoom chatRoom)
        {
            using (var context = new AppDbContext())
            {
                context.ChatRooms.Add(chatRoom);
                context.SaveChanges();
                return new DataResult<ChatRoom>(true, "ChatRoom added successfully", chatRoom);
            }
        }

        public DataResult<List<ChatRoomVM>> GetChatRoomVMs(int apartmentComplexId)
        {
            using (var context = new AppDbContext())
            {
                var result = (
                    from cr in context.ChatRooms
                    join m in context.Messages on cr.LastSeenMessageId equals m.Id into messages
                    from message in messages.DefaultIfEmpty()
                    join t in context.Tenants on cr.TenantId equals t.Id
                    join a in context.Apartments on t.ApartmentId equals a.Id

                    where a.ApartmentComplexId == apartmentComplexId

                    select new ChatRoomVM
                    {
                        ChatRoomId = cr.Id,
                        LastSeenMessageId = message.Id,
                        LastSeenMessage = message.Text,
                        TenantId = t.Id,
                        TenantName = t.Name,
                        TenantLastName = t.LastName,
                        ApartmentId = a.Id,
                        BlockName = a.BlockName,
                        FlatNumber = a.Number,
                        LastChatTime = message.MessageTime,
                        UnreadMessageCount = context.Messages.Count(m => m.ChatRoomId == cr.Id && m.Id > cr.LastSeenMessageId)

                    }).Distinct().OrderByDescending(c => c.LastChatTime).OrderByDescending(c => c.UnreadMessageCount);

                return new DataResult<List<ChatRoomVM>>(true, "Chat rooms listed successfully", result.ToList());
            }
        }

        public Result UpdateLastSeenMessageIdWithNewMax(int chatRoomId, int messageId)
        {
            using var context = new AppDbContext();
            var result = from cr in context.ChatRooms
                         join m in context.Messages on cr.Id equals m.ChatRoomId into messages
                         where cr.Id == chatRoomId
                         select new
                         {
                             newId = (messages.Count() > 1) ? messages.Where(m => m.Id != messageId).Max(m => m.Id) : -1
                         };

            int? newLastSeenMessageId = result.First().newId != -1 ? result.First().newId : null;

            var chatRoom = context.ChatRooms.Find(chatRoomId);
            chatRoom.LastSeenMessageId = newLastSeenMessageId;
            context.SaveChanges();


            return new Result(true, "LastSeenMessageId updated successfully");
        }
    }
}
