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

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
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

            var result = messageService.GetChatRooms(apartmentComplexId);
            if (result.Success && tenantId != null && result.Data.Any(c => c.TenantId == tenantId) == false)
            {
                result.Data.Add(messageService.NewChatRoom((int)tenantId).Data);
            }
            ViewBag.tenantId = tenantId;
            return View(result.Data);
        }

        [HttpPost]
        public PartialViewResult MessageList(int tenantId)
        {
            var result = messageService.GetAllMessagesOfConversation(tenantId);
            ViewBag.tenantId = tenantId;
            return PartialView(result.Data);
        }

        [HttpPost]
        public PartialViewResult MessageItem(MessageDTO messageDTO)
        {

            messageDTO.MessageTime = DateTime.Now;

            messageService.Add(messageDTO);
            return PartialView(messageDTO);
        }

        [HttpGet]
        public PartialViewResult MessageItem(int tenantId, int messageId)
        {
            var result = messageService.GetNewMessagesOfConversation(tenantId, messageId);
            if (result.Data.Count == 0)
                return PartialView(null);

            return PartialView(result.Data.First());
        }

        [HttpDelete]
        public Result Delete(int messageId)
        {
            var result = messageService.DeleteById(messageId);
            return result;
        }

    }
}
