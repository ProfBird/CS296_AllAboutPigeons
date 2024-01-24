﻿using AllAboutPigeons.Data;
using AllAboutPigeons.Models;
using Microsoft.AspNetCore.Identity;

namespace AllAboutPigeons
{
    public class SeedData
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
            if (!context.Messages.Any())  // this is to prevent adding duplicate data
            {
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                var user1 = new AppUser { Name = "Brian Bird", UserName = "Brian" };
                var user2 = new AppUser { Name = "Amanda Bird", UserName = "Amanda" };
                const string SECRET_PASSWORD = "Secret!123";
                // Note: we're not using async/await here,
                // just using the Result property to make the call synchronous
                // and Success to check for successful creation of a user
                bool isSuccess = userManager.CreateAsync(user1, SECRET_PASSWORD).Result.Succeeded;
                isSuccess &= userManager.CreateAsync(user2, SECRET_PASSWORD).Result.Succeeded;

                if (isSuccess)
                {
                    var message1 = new Message
                    {
                        To = user1,
                        From = user2,
                        Text = "This is a test message",
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rating = 10
                    };
                    context.Messages.Add(message1);

                    var message2 = new Message
                    {
                        To = user2,
                        From = user1,
                        Text = "This is another test message",
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rating = 9
                    };
                    context.Messages.Add(message2);

                    context.SaveChanges();
                }
            }
        }
    }
}
