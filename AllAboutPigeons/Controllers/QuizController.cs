using AllAboutPigeons.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllAboutPigeons.Controllers
{
    public class QuizController : Controller
    {
        public Dictionary<int, String> Questions { get; set; }
        public Dictionary<int, String> Answers { get; set; }

        public QuizController()
        {
            // Temporary set of hard-coded questions
            // In the future we'll read these from a file.
            Questions = new Dictionary<int, String>();
            Answers = new Dictionary<int, String>();
            Questions[1] = "Are all pigeons homing pigeons? (Yes or No)";
            Answers[1] = "No";
            Questions[2] = "Are all pigeons secretly government spies? (Yes, No, Some)";
            Answers[2] = "Some";
            Questions[3] = "Where do pigeons sleep (Name an official building)";
            Answers[3] = "The Pentagon";
        }

        public IActionResult Index()
        {
            var model = LoadQuestions(new QuizQuestions());
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string answer1, string answer2, string answer3) 
        { 
            var model = LoadQuestions(new QuizQuestions());
            model.UserAnswers[1] = answer1;
            model.UserAnswers[2] = answer2;
            model.UserAnswers[3] = answer3;
            // Check the user's answers
            var checkedModel = checkQuizAnswers(model);
            return View(checkedModel);
        }

        public QuizQuestions LoadQuestions(QuizQuestions model)
        {
            // load questions and answers into the model
            model.Questions = Questions;
            model.Answers = Answers;
            // TODO: Should these objects be created in the model?
            model.UserAnswers = new Dictionary<int, string>();
            model.Results = new Dictionary<int, bool>();
            // create empty entries for each question
            foreach (var question in Questions)
            {
                int key = question.Key;
                model.UserAnswers[key] = "";
            }

            return model;
        }

        public QuizQuestions checkQuizAnswers(QuizQuestions model) 
        { 
            foreach (var question in Questions) 
            {
                int key = question.Key;
                model.Results[key] = model.Answers[key] == model.UserAnswers[key];
            }
            return model;
        }
    }
}
