﻿using AllAboutPigeons.Data;
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
        public IActionResult ForumPost(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null) // Don't get a user when doing unit tests
            {
                model.From = _userManager.GetUserAsync(User).Result;
            }

            // Temporarily add a random rating to the post
            // TODO: Add a way for users to rate messages, or remove ratings
            Random random = new Random();
            model.Rating = random.Next(0, 10);
            
            // Check to see if the recipient is a registered user
            AppUser recipient = _userManager.FindByNameAsync(model.To.Name).Result;
            if(recipient != null)
            {
                model.To = recipient;
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