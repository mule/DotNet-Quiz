using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetQuizDataAccess.Models
{
    public class Quiz
    {

        public int QuizId { get; set; }
        public DateTime StartTime { get; set; }
        public IEnumerable<Tuple<int,int>> Answers { get; set; }

    }
}