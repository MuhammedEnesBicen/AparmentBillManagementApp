using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using Entity.DTOs;
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

            if (tenantId != null)
            {
                var chatRoomDTO = new ChatRoomDTO
                {
                    TenantId = (int)tenantId,
                    LastSeenMessageId = null,
                };
                var chatRoom = chatRoomService.Add(chatRoomDTO);

                ViewBag.chatRoomId = chatRoom.Data.Id;
            }


            var result = chatRoomService.GetChatRoomVMs(apartmentComplexId).Data;
            if (tenantId != null)
            {
                var cr = result.Find(x => x.TenantId == tenantId);
                result.Remove(cr);
                result.Insert(0, cr);
            }

            return View(result);
        }

        [HttpPost]
        public PartialViewResult MessageList(int chatRoomId)
        {
            var result = messageService.GetAllMessagesOfConversation(chatRoomId);
            if (result.Data.Count != 0)
                chatRoomService.UpdateWithMessageDTO(result.Data.Last());
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
            var result = chatRoomService.UpdateWithMessageDTO(messageDTOFromDBResult.Data);

            return PartialView(messageDTOFromDBResult.Data);
        }

        [HttpGet]
        public PartialViewResult MessageItem(int chatRoomId)
        {
            var result = messageService.GetNewMessagesOfConversation(chatRoomId);
            if (result.Data.Count == 0)
                return PartialView(null);
            chatRoomService.UpdateWithMessageDTO(result.Data.First());
            ViewBag.isMessageNew = true;
            return PartialView(result.Data.First());
        }

        [HttpDelete]
        public Result Delete(int chatRoomId, int messageId)
        {
            var updateChatRoom = chatRoomService.UpdateLastSeenMessageIdWithNewMax(chatRoomId, messageId);

            var result = messageService.DeleteById(messageId);
            return result;
        }

        [HttpGet]
        public int GetUnreadMessageCount(int chatRoomId, int lastSeenMessageId)
        {

            var result = messageService.GetUnreadMessageCount(chatRoomId, lastSeenMessageId);
            return result;
        }

        [HttpGet]
        public int GetTotalUnreadMessageCount()
        {
            int apartmentComplexId = 0;
            bool parsing = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out apartmentComplexId);
            if (parsing is false)
            {
                return 0;
            }
            var result = messageService.GetUnreadMessageCountByApartmentComplexId(apartmentComplexId);
            return result;
        }

    }
}
