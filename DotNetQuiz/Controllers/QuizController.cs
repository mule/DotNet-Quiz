using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DotNetQuiz.Areas.Admin.ViewModels;
using DotNetQuizDataAccess.Models;


namespace DotNetQuiz.Controllers
{
    public class QuizController : Controller
    {
        //
        // GET: /Quiz/

        public ActionResult Index()
        {
            var questionStr =
                @"You are implementing an ASP.NET application that uses data-bound GridView controls in multiple pages. You add JavaScript code to periodically update specific types of data items in these GridView controls. You need to ensure that the JavaScript code can locate the HTML elements created for each row in these GridView controls, without needing to be changed if the controls are moved from one pa to another. What should you do?";

            var answer1 = new Tuple<int, string>(1, "Replace the GridView control with a ListView control.");
            var answer2 = new Tuple<int, string>(2, "Set the ClientIDMode attribute to Predictable in the web.config file.");
            var asnwer3 = new Tuple<int, string>(3, "Set the ClientIDRowSuffix attribute of each unique GridView control to a different value.");
            var answer4 = new Tuple<int, string>(4,
                                                 "Set the @ OutputCache directive's VaryByControl attribute to the ID of the GridView control.");

            var question = new QuestionViewModel()
                               {
                                   QuestionText = questionStr,
                                   AnswerOptions =
                                       new List<Tuple<int, string>> { answer1,answer2,asnwer3,answer4},
                                       AnswerType = Question.AnswerType.SingleChoice

                               };

            return View(question);
        }

        [HttpPost]
        public ActionResult Answer(int question, ICollection<int> answers, int quizId)
        {

            return Json(new {correct = false, message = "Test message"});

        }

        [HttpPost]
        public ActionResult NextQuestion()
        {

                        var questionStr =
                @"You are implementing an ASP.NET application that uses data-bound GridView controls in multiple pages. You add JavaScript code to periodically update specific types of data items in these GridView controls. You need to ensure that the JavaScript code can locate the HTML elements created for each row in these GridView controls, without needing to be changed if the controls are moved from one pa to another. What should you do?";

            var answer1 = new Tuple<int, string>(1, "Replace the GridView control with a ListView control.");
            var answer2 = new Tuple<int, string>(2, "Set the ClientIDMode attribute to Predictable in the web.config file.");
            var asnwer3 = new Tuple<int, string>(3, "Set the ClientIDRowSuffix attribute of each unique GridView control to a different value.");
            var answer4 = new Tuple<int, string>(4,
                                                 "Set the @ OutputCache directive's VaryByControl attribute to the ID of the GridView control.");

            var question = new QuestionViewModel()
                               {
                                   Id = "1",
                                   QuestionText = questionStr,
                                   AnswerOptions =
                                       new List<Tuple<int, string>> { answer1,answer2,asnwer3,answer4},
                                       AnswerType = Question.AnswerType.MultipleChoice


                               };

            return Json(question);



        }

        [HttpPost]
        public ActionResult NewQuiz()
        {
            if(HttpContext.Response.Cookies["DotNetQuiz"]==null)
                HttpContext.Response.Cookies.Add(new HttpCookie("DotNetQuiz","1"));
            else
            {
                HttpContext.Response.Cookies["DotNetQuiz"].Value = "1";

            }

            var quiz = new Quiz() {Id = "1", StartTime = DateTime.Now};


            return Json(quiz);

        }

        [HttpPost]
        public ActionResult QuizStatus(string quizId)
        {

            var quiz = new Quiz() { Id = quizId, StartTime = DateTime.Now };

            return Json(quiz);

        }


    }
}
