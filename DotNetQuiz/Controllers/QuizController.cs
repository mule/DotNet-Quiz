using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DotNetQuiz.ViewModels;
using DotNetQuizDataAccess;
using DotNetQuizDataAccess.Models;
using QuestionViewModel = DotNetQuiz.Areas.Admin.ViewModels.QuestionViewModel;


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
            var answer2 = new Tuple<int, string>(2,
                                                 "Set the ClientIDMode attribute to Predictable in the web.config file.");
            var asnwer3 = new Tuple<int, string>(3,
                                                 "Set the ClientIDRowSuffix attribute of each unique GridView control to a different value.");
            var answer4 = new Tuple<int, string>(4,
                                                 "Set the @ OutputCache directive's VaryByControl attribute to the ID of the GridView control.");

            var question = new QuestionViewModel
                               {
                                   QuestionText = questionStr,
                                   AnswerOptions =
                                       new List<Tuple<int, string>> { answer1, answer2, asnwer3, answer4 },
                                   AnswerType = Question.AnswerType.SingleChoice

                               };

            return View(question);
        }

        [HttpPost]
        public ActionResult Answer(int questionIndx, ICollection<int> answers, string quizId)
        {

         var quiz =   QuizManager.Load(quizId);

         if (quiz == null)
         {
            
             string errorMsg = "Quiz not found";

             return Json(new {error = true, message = errorMsg});

         }

          
                
            return Json(new { correct = quiz.CheckAnswer(questionIndx,answers), message = "Test message", nextQuestionIndx = quiz.NextQuestionIndex, completed = quiz.Completed, error = false });

        }

        //[HttpPost]
        //public ActionResult NextQuestion(string quizId)
        //{

        //    var quiz = QuizManager.Load(quizId);

        //    if (quiz == null)
        //    {

        //        return null;
        //    }

        //    var question = quiz.GetNextQuestion();

        //    if (question == null)
        //        throw new NotImplementedException(); //TODO: Redirect to quiz ended page here




        //    //            var questionStr =
        //    //    @"You are implementing an ASP.NET application that uses data-bound GridView controls in multiple pages. You add JavaScript code to periodically update specific types of data items in these GridView controls. You need to ensure that the JavaScript code can locate the HTML elements created for each row in these GridView controls, without needing to be changed if the controls are moved from one pa to another. What should you do?";

        //    //var answer1 = new Tuple<int, string>(1, "Replace the GridView control with a ListView control.");
        //    //var answer2 = new Tuple<int, string>(2, "Set the ClientIDMode attribute to Predictable in the web.config file.");
        //    //var asnwer3 = new Tuple<int, string>(3, "Set the ClientIDRowSuffix attribute of each unique GridView control to a different value.");
        //    //var answer4 = new Tuple<int, string>(4,
        //    //                                     "Set the @ OutputCache directive's VaryByControl attribute to the ID of the GridView control.");

        //    //var question = new QuestionViewModel()
        //    //                   {
        //    //                       Id = "1",
        //    //                       QuestionText = questionStr,
        //    //                       AnswerOptions =
        //    //                           new List<Tuple<int, string>> { answer1,answer2,asnwer3,answer4},
        //    //                           AnswerType = Question.AnswerType.MultipleChoice


        //    //                   };

        //    return Json(question);



        //}

        [HttpPost]
        public ActionResult NewQuiz()
        {
            var quiz = QuizManager.CreateQuickQuiz();

            if (quiz == null)
                return null; //TODO Add error handling here



            var quizVm = createQuizVMFromModel(quiz);


            if (HttpContext.Response.Cookies["DotNetQuiz"] == null)
                HttpContext.Response.Cookies.Add(new HttpCookie("DotNetQuiz", quizVm.Id));
            else
            {
                HttpContext.Response.Cookies["DotNetQuiz"].Value = quizVm.Id;

            }



            return Json(quizVm);

        }

        [HttpPost]
        public ActionResult QuizStatus(string quizId)
        {

            var quiz = QuizManager.Load(quizId);

            if (quiz == null)
                return null;

           
            return Json(new {Completed = quiz.Completed, NextQuestion = quiz.NextQuestionIndex});

        }

        [HttpPost]
        public ActionResult Results(string quizId)
        {

            var quiz = QuizManager.Load(quizId);

            if (quiz == null)
                return Json(new {error = true, message = "quiz not found"});


            return Json(new {correctAnswers = quiz.CorrectlyAnsweredQuestions.Count(), total = quiz.Questions.Count()});


        }



        private QuizViewModel createQuizVMFromModel(Quiz quiz)
        {

            var quizVm = new QuizViewModel();

            quizVm.Id = quiz.Id;
            quizVm.StartTime = quiz.StartTime;
            quizVm.Completed = quiz.Completed;


            var questionList = new List<QuestionViewModel>();

            foreach (Question question in quiz.Questions)
            {
                var questionVm = new QuestionViewModel
                                     {
                                         Id = question.Id,
                                         QuestionText = question.QuestionText,
                                         AnswerType = question.QuestionAnswerType,
                                         AnswerOptions = new List<Tuple<int, string>>()
                                     };


                foreach (Tuple<int, string, bool> answerOption in question.AnswerOptions)
                {
                    questionVm.AnswerOptions.Add(new Tuple<int, string>(answerOption.Item1, answerOption.Item2));

                }

                questionList.Add(questionVm);
            }

            quizVm.Questions = questionList;


            return quizVm;

        }
    }
}
