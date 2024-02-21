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
        public IActionResult ForumPost() 
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult>  ForumPost(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null) // Don't get a user when doing unit tests
            {
                // Get the sender
                model.From = _userManager.GetUserAsync(User).Result;
                // Check to see if the recipient user name matches a registered user
                AppUser recipient = await _userManager.FindByNameAsync(model.To.Name);
                if (recipient != null)
                {
                    model.To = recipient;
                }
            }

            // Temporarily add a random rating to the post
            // TODO: Add a way for users to rate messages, or remove ratings
            Random random = new Random();
            model.Rating = random.Next(0, 10);
            
            if(model.To.UserName != "")  // check for valid recipient
            {
                // Save the message
               await _repository.StoreMessageAsync(model);
                return RedirectToAction("Index", new { model.MessageId });
            }
            else
            {
                ModelState.AddModelError("", "Recipient not a registered user");
                return View(model);
            }
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
