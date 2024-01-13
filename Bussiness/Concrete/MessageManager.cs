using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;

namespace Bussiness.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal messageDal;
        private readonly IMapper mapper;

        public MessageManager(IMessageDal messageDal, IMapper mapper)
        {
            this.messageDal = messageDal;
            this.mapper = mapper;
        }

        public Result Add(MessageDTO messageDTO)
        {
            var message = mapper.Map<Message>(messageDTO);
            messageDal.Add(message);
            return new Result(true, "Message sended successfully");
        }


        public Result DeleteById(int messageId)
        {
            var messageFromDb = messageDal.Get(m => m.Id == messageId);
            if (messageFromDb == null)
            {
                return new Result(false, "Message not found");
            }
            messageDal.Delete(messageFromDb);
            return new Result(true, "Message deleted successfully");
        }

        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int tenantId)
        {
            var result = messageDal.GetAllMessagesOfConversation(tenantId);
            return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.Data);
        }

        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int tenantId, int messageId)
        {
            var result = messageDal.GetNewMessagesOfConversation(tenantId, messageId);
            return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.Data);
        }
    }
}
