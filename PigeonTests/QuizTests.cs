
using AllAboutPigeons.Controllers;
using AllAboutPigeons.Models;

namespace PigeonTests
{
    public class QuizTests
    {
        [Fact]
        public void TestLoadQuestions()
        {
            // Arrange
            var controller = new QuizController();
            var model = new QuizQuestions();

            // Act
            var loadedModel = controller.LoadQuestions(model);

            // Assert
            Assert.NotNull(loadedModel.Questions);
            Assert.NotNull(loadedModel.Answers);
            Assert.NotEmpty(loadedModel.Questions);
            Assert.NotEmpty(loadedModel.Answers); Assert.Equal(controller.Questions, loadedModel.Questions);
            Assert.Equal(controller.Answers, loadedModel.Answers);
            Assert.Equal(loadedModel.Questions.Count, loadedModel.Answers.Count);
        }

        [Fact]
        public void TestCheckQuizAnswers()
        {
            // Arrange
            // create model, put in some right and wrong answer
            var model = new QuizQuestions();
            var controller = new QuizController();
            var loadedModel = controller.LoadQuestions(model);
            loadedModel.UserAnswers[1] = "No";  // true
            loadedModel.UserAnswers[2] = "Yes"; // false
            loadedModel.UserAnswers[3] = ""; // no answer, false

            // Act
            var result = controller.checkQuizAnswers(model);
            // Assert
            // user's right answers are shown to be right
            Assert.True(result.Results[1]);

            // user's wrong answer are shown to be wrong
            Assert.False(result.Results[2]);
            Assert.False(result.Results[3]);
        }
    }
}