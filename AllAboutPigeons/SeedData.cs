using AllAboutPigeons.Data;
using AllAboutPigeons.Models;

namespace AllAboutPigeons
{
    public class SeedData
    {
        public static void Seed(AppDbContext context)

        {
            if (!context.Messages.Any())  // this is to prevent adding duplicate data
            {
                var user1 = new AppUser { Name = "Brian" };
                var user2 = new AppUser { Name = "Amanda" };
                context.Users.Add(user1);
                context.Users.Add(user2);
                context.SaveChanges();

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
