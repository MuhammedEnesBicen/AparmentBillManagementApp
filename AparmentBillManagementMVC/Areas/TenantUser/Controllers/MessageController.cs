using Bussiness.Abstract;
using Core.Utilities;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Areas.TenantUser.Controllers
{
    [Area("TenantUser")]
    [Authorize(Roles = "tenant")]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IChatRoomService chatRoomService;
        public MessageController(IMessageService messageService, IChatRoomService chatRoomService)
        {
            this.messageService = messageService;
            this.chatRoomService = chatRoomService;
        }

        public IActionResult Index()
        {
            int tenantId;
            var getIdViaClaim = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out tenantId);
            if (!getIdViaClaim)
            {
                TempData["message"] = "You must login first";
                return RedirectToAction("Login", "Auth", new { area = "Identity" });
            }

            var newChatRoom = new ChatRoomDTO
            {
                TenantId = tenantId,
                LastSeenMessageId = null,
            };
            var chatRoomResult = chatRoomService.Add(newChatRoom);
            if (!chatRoomResult.Success)
            {
                TempData["message"] = chatRoomResult.Message;
                return RedirectToAction("Index", "Home", new { area = "TenantUser" });
            }
            ViewBag.chatRoomId = chatRoomResult.Data.Id;
            return View();
        }

        [HttpPost]
        public PartialViewResult Messages(int chatRoomId)
        {
            var result = messageService.GetAllMessagesOfConversation(chatRoomId);
            if (result.Success && result.Data.Count > 0)
                SetTenantsLastReadedMessageIdViaCookie(result.Data.Last().Id);
            return PartialView(result.Data);
        }

        [HttpPost]
        public PartialViewResult Message(MessageDTO messageDTO)
        {
            messageDTO.MessageTime = DateTime.Now;

            var result = messageService.Add(messageDTO);
            if (!result.Success)
                return PartialView(null);

            SetTenantsLastReadedMessageIdViaCookie(result.Data.Id);
            return PartialView(result.Data);
        }

        [HttpGet]
        public PartialViewResult Message(int chatRoomId, int lastMessageId)
        {
            var result = messageService.GetNewMessagesByChatRoomIdAndMessageId(chatRoomId, lastMessageId);
            if (result.Data.Count == 0)
                return PartialView(null);

            SetTenantsLastReadedMessageIdViaCookie(result.Data.First().Id);
            ViewBag.isNewMessage = true;
            return PartialView(result.Data.First());
        }

        [HttpDelete]
        public Result Delete(int messageId)
        {
            var result = messageService.DeleteById(messageId);
            return result;
        }

        public int? GetTenantsLastReadedMessageIdViaCookie()
        {
            int lastSeenMessageId;

            bool result = Int32.TryParse(Request.Cookies["lastSeenMessageId"], out lastSeenMessageId);
            if (!result)
                return null;
            return lastSeenMessageId;
        }

        public void SetTenantsLastReadedMessageIdViaCookie(int lastSeenMessageId)
        {
            Response.Cookies.Append("lastSeenMessageId", lastSeenMessageId.ToString());
        }

        [HttpGet]
        public int GetUnreadMessagesCount()
        {
            int tenantId;
            var getIdViaClaim = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out tenantId);
            if (!getIdViaClaim)
            {
                return -1;
            }
            var chatRoom = chatRoomService.GetByTenantId(tenantId);
            if (chatRoom.Data is null)
                return 0;
            int lastSeenMessageId = GetTenantsLastReadedMessageIdViaCookie() ?? 0;
            var result = messageService.GetUnreadMessageCount(chatRoom.Data.Id, lastSeenMessageId);

            return result;
        }

    }
}
