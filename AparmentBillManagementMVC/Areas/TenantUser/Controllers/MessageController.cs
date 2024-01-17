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

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
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
            ViewBag.tenantId = tenantId;
            return View();
        }

        [HttpPost]
        public PartialViewResult Messages(int tenantId)
        {
            var result = messageService.GetAllMessagesOfConversation(tenantId);
            return PartialView(result.Data);
        }

        [HttpPost]
        public PartialViewResult Message(MessageDTO messageDTO)
        {
            messageDTO.MessageTime = DateTime.Now;

            var result = messageService.Add(messageDTO);
            if (!result.Success)
                return PartialView(null);
            return PartialView(result.Data);
        }

        [HttpGet]
        public PartialViewResult Message(int tenantId, int messageId)
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
