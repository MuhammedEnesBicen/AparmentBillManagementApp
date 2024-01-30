using Core.DataAccess.EntityFramework;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, AppDbContext>, IMessageDal
    {
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int chatRoomID)
        {
            using (var context = new AppDbContext())
            {
                var result = from m in context.Messages
                             join c in context.ChatRooms on m.ChatRoomId equals c.Id
                             where m.ChatRoomId == chatRoomID
                             select new MessageDTO
                             {
                                 Id = m.Id,
                                 Text = m.Text,
                                 MessageTime = m.MessageTime,
                                 Sender = m.Sender,
                                 ChatRoomId = m.ChatRoomId
                             };
                return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.ToList());
            }
        }

        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int chatRoomId)
        {
            using (var context = new AppDbContext())
            {
                var a = from m in context.Messages
                        join c in context.ChatRooms on m.ChatRoomId equals c.Id
                        where m.Id > c.LastSeenMessageId && m.ChatRoomId == chatRoomId
                        select new MessageDTO
                        {
                            Id = m.Id,
                            Text = m.Text,
                            MessageTime = m.MessageTime,
                            Sender = m.Sender,
                            ChatRoomId = m.ChatRoomId
                        };
                return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", a.ToList());
            }
        }

        public int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId)
        {
            using var context = new AppDbContext();
            var result = context.Messages.Where(m => m.ChatRoomId == chatRoomId && m.Id > lastSeenMessageId).Count();
            return result;
        }

        DataResult<Message> IMessageDal.Add(Message message)
        {
            using (var context = new AppDbContext())
            {
                var addedEntity = context.Entry(message);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return new DataResult<Message>(true, "Message added successfully", addedEntity.Entity);
            }
        }




    }
}
