using Bussiness.Abstract;
using Core.Utilities;
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
            return View();
        }

        [HttpPost]
        public PartialViewResult MessageList(int tenantId)
        {
            var result = messageService.GetAllMessagesOfConversation(tenantId);
            return PartialView(result.Data);
        }

        [HttpPost]
        public Result SendMessage(int tenantId,string text, int sender)
        {
            //int tenantId;
            //var getIdViaClaim = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,out tenantId);
            //if (!getIdViaClaim)
            //{
            //    return new Result(false, "Cant send message. Try again later. If the problem persists, please log in to the system again.");
            //}

            var messageDTO = new MessageDTO
            {
                Text = text,
                Sender = (UserType)sender,
                MessageTime = DateTime.Now,
                TenantId = tenantId
            };
            return messageService.Add(messageDTO);
        }
    }
}
