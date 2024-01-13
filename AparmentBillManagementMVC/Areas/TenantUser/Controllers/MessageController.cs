using Bussiness.Abstract;
using Core.Utilities;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AparmentBillManagementMVC.Areas.TenantUser.Controllers
{
    [Area("TenantUser")]
    //[Route("TenantUser/[controller]/[action]/{id}")]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            ViewBag.tenantId = 11;
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
            //int tenantId;
            //var getIdViaClaim = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,out tenantId);
            //if (!getIdViaClaim)
            //{
            //    return new Result(false, "Cant send message. Try again later. If the problem persists, please log in to the system again.");
            //}

            messageDTO.MessageTime = DateTime.Now;

            messageService.Add(messageDTO);
            return PartialView(messageDTO);
        }

        [HttpGet]
        public PartialViewResult? Message(int tenantId, int messageId)
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
