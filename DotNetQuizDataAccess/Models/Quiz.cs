using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetQuizDataAccess.Models
{
    public class Quiz
    {

        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Tuple<string,int>> Answers { get; set; }

    }
}