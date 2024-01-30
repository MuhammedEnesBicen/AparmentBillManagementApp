using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using Entity.DTOs;
using Entity.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Controllers
{
    [Authorize(Roles = "manager")]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IChatRoomService chatRoomService;
        private readonly IMapper mapper;
        public MessageController(IMessageService messageService, IChatRoomService chatRoomService, IMapper mapper)
        {
            this.messageService = messageService;
            this.chatRoomService = chatRoomService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int? tenantId)
        {
            int apartmentComplexId;
            var getIdViaClaim = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out apartmentComplexId);
            if (!getIdViaClaim)
            {
                TempData["message"] = "An error occured. Please re login to website.";
                return RedirectToAction("Login", "Auth");
            }

            // TODO: getChatRooms need user type to
            if (tenantId != null)
            {
                var chatRoomDTO = new ChatRoomDTO
                {
                    TenantId = (int)tenantId,
                    LastSeenMessageId = null,
                    User = UserType.manager
                };
                chatRoomService.Add(chatRoomDTO);
            }

            var result = chatRoomService.GetChatRoomVMs(apartmentComplexId).Data.Where(c => c.LastSeenMessageId != null || c.TenantId == tenantId).ToList();

            var chatRoom = result.Find(c => c.TenantId == tenantId);
            ViewBag.chatRoomId = chatRoom?.ChatRoomId;
            return View(result);
        }

        [HttpPost]
        public PartialViewResult MessageList(int chatRoomId)
        {
            var result = messageService.GetAllMessagesOfConversation(chatRoomId);
            if (result.Data.Count != 0)
                chatRoomService.UpdateWithMessageDTO(result.Data.Last(), forWhichUser: UserType.manager);
            ViewBag.chatRoomId = chatRoomId;
            return PartialView(result.Data);
        }

        [HttpPost]
        public PartialViewResult MessageItem(MessageDTO messageDTO)
        {
            // TODO: if messageDTOFromDBResult is not success, return error message


            messageDTO.MessageTime = DateTime.Now;
            var messageDTOFromDBResult = messageService.Add(messageDTO);
            if (!messageDTOFromDBResult.Success)
            {
                return PartialView(null);
            }
            chatRoomService.UpdateWithMessageDTO(messageDTOFromDBResult.Data, forWhichUser: UserType.manager);

            return PartialView(messageDTO);
        }

        [HttpGet]
        public PartialViewResult MessageItem(int chatRoomId)
        {
            var result = messageService.GetNewMessagesOfConversation(chatRoomId);
            if (result.Data.Count == 0)
                return PartialView(null);

            chatRoomService.UpdateWithMessageDTO(result.Data.First(), forWhichUser: UserType.manager);
            ViewBag.isMessageNew = true;
            return PartialView(result.Data.First());
        }

        [HttpDelete]
        public Result Delete(int messageId)
        {
            // TODO: if message that will be deleted is last message of chatroom, update chatroom
            var result = messageService.DeleteById(messageId);
            return result;
        }

        [HttpGet]
        public int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId)
        {

            var result = messageService.GetUnreadMessageCount(chatRoomId, lastSeenMessageId);
            return result;
        }

    }
}
