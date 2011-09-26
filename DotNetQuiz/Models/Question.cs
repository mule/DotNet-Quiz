﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetQuiz.Models
{
    public class Question
    {
        public string QuestionText { get; set; }

        public List<Tuple<int,string>> AnswerOptions { get; set; }
        

    }
}