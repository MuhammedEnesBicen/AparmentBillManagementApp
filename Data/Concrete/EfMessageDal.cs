using Core.DataAccess.EntityFramework;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;

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
    }
}
