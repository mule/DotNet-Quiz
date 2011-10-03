using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetQuiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Tuple<int,string>> AnswerOptions { get; set; }
        public int CorrectAnswer { get; set; }
        

    }
}