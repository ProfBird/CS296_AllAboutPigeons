using AllAboutPigeons.Data;
using AllAboutPigeons.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllAboutPigeons.Controllers
{
    public class MessageController : Controller
    {
       // AppDbContext context;
       readonly IMessageRepository _repository;
       readonly UserManager<AppUser> _userManager;
        public MessageController(IMessageRepository r, UserManager<AppUser> u) 
        {
            _repository = r;
            _userManager = u;
        }

        // TODO: Do something interesting with the messageId
        public IActionResult Index()
        {
            // Get the last post out of the database
           var messages = _repository.GetMessages();
            // .Where(m => m.MessageId == int.Parse(messageId))
            // .FirstOrDefault();
            // .Find(int.Parse(messageId));
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string toname)
        {
            var messages =  _repository.GetMessages()
            .Where(m => m.To.Name == toname)
            .ToList<Message>();                      

            return View("Index", messages);
        }

        [Authorize]
        public IActionResult ForumPost(int? replyId) 
        {
            Message reply = new Message();
            if (replyId != null)
            {
                reply.OriginalMessageId = replyId;
            }
            return View(reply);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult>  ForumPost(Message message)
        {
            message.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null) // Don't get a user when doing unit tests
            {
                // Get the sender
                message.From = _userManager.GetUserAsync(User).Result;
                // Check to see if the recipient user name matches a registered user
                AppUser recipient = await _userManager.FindByNameAsync(message.To.Name);
                if (recipient != null)
                {
                    message.To = recipient;
                }
            }

            // Temporarily add a random rating to the post
            // TODO: Add a way for users to rate messages, or remove ratings
            Random random = new Random();
            message.Rating = random.Next(0, 10);
            
            // If this is a reply, add it to the original message in the database
            if (message.OriginalMessageId != null)
            {
                Message originalMessage = await _repository.GetMessageByIdAsync(message.OriginalMessageId.Value);
                originalMessage.Reply = message;
                await _repository.StoreMessageAsync(originalMessage);
            }
            
            if(message.To.UserName != "")  // check for valid recipient
            {
                // Save the message
               await _repository.StoreMessageAsync(message);
                return RedirectToAction("Index", new { message.MessageId });
            }
            else
            {
                ModelState.AddModelError("", "Recipient not a registered user");
                return View(message);
            }
        }
        
        public async Task<IActionResult> Reply(int messageId)
        {
            /*
            // Get the message to reply to
            Message originalMessage = await _repository.GetMessageByIdAsync(messageId);
            // Create a new reply
            Message reply = new Message();
            reply.OriginalMessage = originalMessage;
            */
            return RedirectToAction("ForumPost", messageId);
        }

        public IActionResult DeleteForumPost(int messageId)
        {
            // TODO: Do something like redirect if the delete fails
            _repository.DeleteMessage(messageId);
            return RedirectToAction("Index");
        }

            public IActionResult Info()
        {
            return View();
        }

    }
}
