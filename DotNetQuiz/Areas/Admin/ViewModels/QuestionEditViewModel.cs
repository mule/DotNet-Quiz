using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetQuizDataAccess.Models;

namespace DotNetQuiz.Areas.Admin.ViewModels
{
    public class QuestionEditViewModel
    {
        public readonly SelectList AnswerTypes;


       
         
        public string Id { get; set; }
        [AllowHtml]
        public string QuestionText { get; set; }
        [AllowHtml]
        public List<AnswerOptionViewModel> AnswerOptions { get; set; }

        public Question.AnswerType AnswerType { get; set; }


        public QuestionEditViewModel()
        {
            var values =
                Enum.GetValues(typeof (Question.AnswerType)).Cast<Question.AnswerType>().Select(
                    e => new KeyValuePair<int, string>((int)e,e.ToString()));


            AnswerTypes = new SelectList(values,"Key","Value");
        }


        

    }
}