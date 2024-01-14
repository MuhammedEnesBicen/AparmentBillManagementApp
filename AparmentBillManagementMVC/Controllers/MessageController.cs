using Bussiness.Abstract;
using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            //TODO get from claim 
            var result = messageService.GetChatRooms(1);
            //ViewBag.tenantId = 11;
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
