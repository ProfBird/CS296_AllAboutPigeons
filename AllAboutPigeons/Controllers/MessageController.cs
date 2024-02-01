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
       IRegistryRepository repository;
        UserManager<AppUser> userManager;
        public MessageController(IRegistryRepository r, UserManager<AppUser> u) 
        {
            repository = r;
            userManager = u;
        }

        // TODO: Do something interesting with the messageId
        public IActionResult Index()
        {
            // Get the last post out of the database
           var messages = repository.GetMessages();
            // .Where(m => m.MessageId == int.Parse(messageId))
            // .FirstOrDefault();
            // .Find(int.Parse(messageId));
            return View(messages);
        }

        [HttpPost]
        public IActionResult Index(string toname)
        {
            List<Message> messages = (from m in repository.GetMessages()
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
        public IActionResult ForumPost(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (userManager != null) // Don't get a user when doing unit tests
            {
                model.From = userManager.GetUserAsync(User).Result;
            }

            // Temporarily add a random rating to the post
            Random random = new Random();
            model.Rating = random.Next(0, 10);

            // Save model to db
            int result;
            result = repository.StoreMessage(model);
            // TODO: Do something with the result
            return RedirectToAction("Index", new { model.MessageId });
        }

            public IActionResult Info()
        {
            return View();
        }

    }
}
