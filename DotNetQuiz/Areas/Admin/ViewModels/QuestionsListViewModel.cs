using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetQuizDataAccess.Models;

namespace DotNetQuiz.Areas.Admin.ViewModels
{
    public class QuestionsListViewModel
    {


        public IEnumerable<Question> Questions { get; private set; }



        public QuestionsListViewModel()
        {
                Questions = new List<Question>();
        }







    }
}