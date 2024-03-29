﻿using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
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

        public DataResult<MessageDTO> Add(MessageDTO messageDTO)
        {
            var message = mapper.Map<Message>(messageDTO);
            var result = messageDal.Add(message);
            mapper.Map<Message, MessageDTO>(result.Data, messageDTO);
            return new DataResult<MessageDTO>(true, "Message sended successfully", messageDTO);
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

        public DataResult<List<MessageDTO>> GetAllMessagesOfConversation(int chatRoomId)
        {
            var result = messageDal.GetAllMessagesOfConversation(chatRoomId);
            return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.Data);
        }

        public DataResult<List<MessageDTO>> GetNewMessagesByChatRoomIdAndMessageId(int chatRoomId, int messageId)
        {
            var result = messageDal.GetList(m => m.ChatRoomId == chatRoomId && m.Id > messageId);
            return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", mapper.Map<List<MessageDTO>>(result.ToList()));
        }

        public DataResult<List<MessageDTO>> GetNewMessagesOfConversation(int chatRoomId)
        {
            var result = messageDal.GetNewMessagesOfConversation(chatRoomId);
            return new DataResult<List<MessageDTO>>(true, "Messages listed successfully", result.Data);
        }

        public int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId)
        {
            return messageDal.GetUnreadMessageCount(chatRoomId, lastSeenMessageId);
        }

        public int GetUnreadMessageCountByApartmentComplexId(int apartmentComplexId)
        {
            return messageDal.GetUnreadMessageCountByApartmentComplexId(apartmentComplexId);
        }
    }
}
