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
            var messages = _repository.GetMessages();
            // Get the last post out of the database
            // .Where(m => m.MessageId == int.Parse(messageId))
            // .FirstOrDefault();
            // .Find(int.Parse(messageId));
            return View(messages);
        }

        [HttpPost]
        public IActionResult Index(string toname)
        {
            var messages = _repository.GetMessages()
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
        public async Task<IActionResult> ForumPost(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null) // Don't get a user when doing unit tests
            {
                // Get the sender
                model.From = _userManager.GetUserAsync(User).Result;
                // Check to see if the recipient user name matches a registered user
                AppUser recipient = _userManager.FindByNameAsync(model.To.Name).Result;
                if (recipient != null)
                {
                    model.To = recipient;
                }
            }

            // Temporarily add a random rating to the post
            // TODO: Add a way for users to rate messages, or remove ratings
            Random random = new Random();
            model.Rating = random.Next(0, 10);

            if (model.To.UserName != null) // check for valid recipient
            {
                // Save the message
                await _repository.StoreMessageAsync(model);
                //TODO: Do something interesting/useful with the MessageId or don't send it. It's not currently used.
                return RedirectToAction("Index", new { model.MessageId });
            }
            else
            {
                ModelState.AddModelError("", "Recipient not a registered user");
                return View(model);
            }
        }

        [Authorize]
        public IActionResult Reply(int? OriginalMessageId)
        {
            Message reply = new Message();
            reply.OriginalMessageId = OriginalMessageId;
            return View(reply);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reply(Message reply)
        {
            reply.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null) // Don't get a user when doing unit tests
            {
                // Get the sender
                reply.From = _userManager.GetUserAsync(User).Result;
            }
            // Get the message being replied to (guarantted to be not null by design)
            Message originalMessage = await _repository.GetMessageByIdAsync(reply.OriginalMessageId.Value);
            // Get the recipient
            reply.To = originalMessage.From;
            // Save the message
            await _repository.StoreMessageAsync(reply);
            // Add the reply to the original message
            originalMessage.Replies.Add(reply);
            _repository.UpdateMessage(originalMessage);
            //TODO: Do something interesting/useful with the MessageId or don't send it. It's not currently used.
            return RedirectToAction("Index", new { reply.MessageId });
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