using AllAboutPigeons.Controllers;
using AllAboutPigeons.Data;
using AllAboutPigeons.Models;

namespace PigeonTests
{
    public class RegistryTests
    {
        [Fact]
        public void ForumPostTest()
        {
            // Arrange: create a controller object and model object
            var repo = new FakeRegistryRepository();
            var controller = new RegistryController(repo);
            var model = new Message()
            {
                From = new AppUser { Name = "Tester", },
                To = new AppUser { Name = "Another Tester" },
                Text = "This is a test"
            };
                

            // Act: pass a model to the controller ForumPost method
            controller.ForumPost(model);

            // Assert: Id, date, and rating got added to the model
            Assert.True(model.MessageId > 0);
            Assert.Equal(model.Date.ToShortDateString(),
                DateTime.Now.ToShortDateString());
            Assert.InRange(model.Rating,1, 10);
        }
    }
}
