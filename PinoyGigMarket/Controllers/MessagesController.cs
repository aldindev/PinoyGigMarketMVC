using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PinoyGigMarket.Data;
using PinoyGigMarket.Data.Repositories;
using PinoyGigMarket.Models;
using PinoyGigMarket.ViewModels;

namespace PinoyGigMarket.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;

        public MessagesController(IMessageService messageService, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var sentMessages = await _messageService.GetUserMessages(userId, isSent: true);
            var receivedMessages = await _messageService.GetUserMessages(userId, isSent: false);

            var viewModel = new SentReceivedViewModel
            {
                SentMessages = sentMessages,
                ReceivedMessages = receivedMessages
            };

            return View(viewModel);
        }

        public IActionResult Send(string receiverId)
        {
            var receiver = _userManager.Users.FirstOrDefault(u => u.Id == receiverId);
            if (receiver == null)
            {
                return NotFound();
            }

            var model = new MessageViewModel
            {
                ReceiverId = receiver.Id,
                ReceiverFirstName = receiver.FirstName,
                ReceiverLastName = receiver.LastName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Send(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var senderId = _userManager.GetUserId(User);
                await _messageService.SendMessage(senderId, model.ReceiverId, model.Content);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Details(int id)
        //{
        //    var message = await _messageService.GetMessageById(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    // Optionally mark the message as read
        //    message.IsRead = true;
        //    await _messageService.SendMessage(message);

        //    return View(message);
        //}


    }
}
