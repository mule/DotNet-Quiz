using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetQuiz.Areas.Admin.ViewModels
{
    public class AnswerOptionViewModel
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
    }
}