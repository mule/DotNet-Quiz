using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetQuizDataAccess.Models;

namespace DotNetQuiz.Areas.Admin.ViewModels
{
    public class QuestionViewModel
    {
        public string Id { get; set; }
        public string QuestionText { get; set; }
        public Question.AnswerType AnswerType { get; set; }
        public List<Tuple<int,string>> AnswerOptions { get; set; }
    }
}