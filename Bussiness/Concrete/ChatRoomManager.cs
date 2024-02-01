using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace Bussiness.Concrete
{
    public class ChatRoomManager : IChatRoomService
    {
        private readonly IChatRoomDal chatRoomDal;
        private readonly IMapper mapper;

        public ChatRoomManager(IChatRoomDal chatRoomDal, IMapper mapper)
        {
            this.chatRoomDal = chatRoomDal;
            this.mapper = mapper;
        }

        public DataResult<ChatRoom> Add(ChatRoomDTO chatRoomDTO)
        {
            var cr = GetByTenantId(chatRoomDTO.TenantId);
            if (cr.Success)
            {
                return new DataResult<ChatRoom>(true, "ChatRoom already exists", cr.Data);
            }
            var newChatRoom = mapper.Map<ChatRoom>(chatRoomDTO);
            return chatRoomDal.Add(newChatRoom);
        }

        public Result Delete(ChatRoom chatRoom)
        {
            chatRoomDal.Delete(chatRoom);
            return new Result(true, "ChatRoom deleted successfully");
        }

        public Result DeleteById(int id)
        {
            var chatRoomFromDbResult = GetById(id);
            if (!chatRoomFromDbResult.Success)
            {
                return new Result(false, "ChatRoom not found");
            }

            return Delete(chatRoomFromDbResult.Data);

        }

        public DataResult<ChatRoom> GetById(int chatRoomId)
        {
            var chatRoomFromDB = chatRoomDal.Get(x => x.Id == chatRoomId);
            if (chatRoomFromDB == null)
            {
                return new DataResult<ChatRoom>(false, "ChatRoom not found", null);
            }
            return new DataResult<ChatRoom>(true, "ChatRoom found", chatRoomFromDB);
        }

        public DataResult<ChatRoom> GetByTenantId(int tenantId)
        {
            var cr = chatRoomDal.Get(x => x.TenantId == tenantId);
            if (cr is null)
            {
                return new DataResult<ChatRoom>(false, "ChatRoom not found", null);
            }
            return new DataResult<ChatRoom>(true, "ChatRoom found", cr);
        }

        public DataResult<List<ChatRoomVM>> GetChatRoomVMs(int apartmentComplexID)
        {
            return chatRoomDal.GetChatRoomVMs(apartmentComplexID);
        }

        public Result UpdateLastSeenMessageIdWithNewMax(int chatRoomId, int messageId)
        {
            return chatRoomDal.UpdateLastSeenMessageIdWithNewMax(chatRoomId, messageId);
        }

        public Result UpdateWithMessageDTO(MessageDTO messageDTO)
        {
            var chatRoomFromDb = chatRoomDal.Get(x => x.Id == messageDTO.ChatRoomId);
            chatRoomFromDb.LastSeenMessageId = messageDTO.Id;
            chatRoomDal.Update(chatRoomFromDb);
            return new Result(true, "ChatRoom updated successfully");
        }
    }
}
