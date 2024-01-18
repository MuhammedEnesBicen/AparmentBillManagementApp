using Core.DataAccess.EntityFramework;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, AppDbContext>, IMessageDal
    {
        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int tenantId)
        {
            using (var context = new AppDbContext())
            {
                var result = from m in context.Messages
                             where m.TenantId == tenantId
                             select new MessageDTO
                             {
                                 Id = m.Id,
                                 Text = m.Text,
                                 MessageTime = m.MessageTime,
                                 Sender = m.Sender,
                                 TenantId = m.TenantId
                             };
                return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.ToList());
            }
        }

        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int tenantId, int messageId)
        {
            using (var context = new AppDbContext())
            {
                var result = from m in context.Messages
                             where m.TenantId == tenantId && m.Id > messageId
                             select new MessageDTO
                             {
                                 Id = m.Id,
                                 Text = m.Text,
                                 MessageTime = m.MessageTime,
                                 Sender = m.Sender,
                                 TenantId = m.TenantId
                             };
                return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.ToList());
            }
        }

        public DataResult<List<ChatRoomVM>> GetChatRooms(int apartmentComplexId)
        {
            using (var context = new AppDbContext())
            {
                var result = (
                              from a in context.Apartments
                              join t in context.Tenants on a.Id equals t.ApartmentId
                              join m in context.Messages on t.Id equals m.TenantId into messages
                              where a.ApartmentComplexId == apartmentComplexId

                              select new ChatRoomVM
                              {

                                  TenantId = t.Id,
                                  TenantName = t.Name,
                                  TenantLastName = t.LastName,
                                  ApartmentId = a.Id,
                                  BlockName = a.BlockName,
                                  FlatNumber = a.Number,
                                  LastChatTime = messages.Max(m => m.MessageTime),

                              }).Distinct().OrderByDescending(c => c.LastChatTime);

                return new DataResult<List<ChatRoomVM>>(true, "Chat rooms listed successfully", result.ToList());
            }
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

        public DataResult<ChatRoomVM> NewChatRoom(int tenantId)
        {
            using (var context = new AppDbContext())
            {
                var result =
                              from t in context.Tenants
                              join a in context.Apartments on t.ApartmentId equals a.Id
                              where t.Id == tenantId

                              select new ChatRoomVM
                              {

                                  TenantId = t.Id,
                                  TenantName = t.Name,
                                  TenantLastName = t.LastName,
                                  ApartmentId = a.Id,
                                  BlockName = a.BlockName,
                                  FlatNumber = a.Number,
                                  LastChatTime = DateTime.Now,

                              };

                return new DataResult<ChatRoomVM>(true, "Chat room generated", result.First());
            }
        }
    }
}
