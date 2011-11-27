using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetQuiz.Areas.Admin.ViewModels;

namespace DotNetQuiz.ViewModels
{
    public class QuizViewModel
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }





        public bool Completed { get; set; }
    }
}