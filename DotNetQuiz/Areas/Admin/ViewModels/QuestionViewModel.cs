using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetQuiz.Areas.Admin.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Tuple<int,string>> AnswerOptions { get; set; }
    }
}