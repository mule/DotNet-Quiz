using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetQuiz.Areas.Admin.ViewModels
{
    public class AnswerOptionViewModel
    {
        public int AnswerId { get; set; }
        [AllowHtml]
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
    }
}