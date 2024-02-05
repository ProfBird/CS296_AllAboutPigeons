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
        public IActionResult Index(string toname)
        {
            List<Message> messages = (from m in _repository.GetMessages()
            where m.To.Name == toname
            select m).ToList();

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
                _repository.StoreMessage(model);
                return RedirectToAction("Index", new { model.MessageId });
            }
            else
            {
                ModelState.AddModelError("", "Recipient not a registered user");
                return View(model);
            }
        }

            public IActionResult Info()
        {
            return View();
        }

    }
}
