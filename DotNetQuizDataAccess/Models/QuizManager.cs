using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetQuizDataAccess.Interface;
using DotNetQuizDataAccess.Models;
using StructureMap;

namespace DotNetQuizDataAccess
{
   public  class QuizManager
    {

       
        private static IRepository<Quiz> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Quiz>>();
            }

        }

        public static Quiz Load(string id)
        {

            var quiz =  _repository.Single(u => u.Id == id);

            if (quiz == null)
                return null;

            if (checkCompletionStatus(ref quiz))
            {
                Insert(quiz);

                UnitOfWork.Commit();
            }

            return quiz;




        }

        public static List<Quiz> GetAll()
        {
            List<Quiz> list = new List<Quiz>();

            // Fetch all entities from the repository.
            list = _repository.GetAll().ToList();

            return list;
        }

        public static void Insert(Quiz entity)
        {
            // Add the new entity to the repository.
            _repository.Add(entity);

        }

        public static void Delete(Quiz entity)
        {
            // Add any custom business rules.
            _repository.Delete(entity);
        }

       public static Quiz CreateQuickQuiz()
       {
           var quiz = new Quiz {StartTime = DateTime.Now};
           var questions = QuestionManager.GetAll().ToArray();

           int lastIndex = questions.Count() - 1;
           var random = new Random(DateTime.Now.Millisecond);

           var quizQuestionIndexes = new List<int>();

           //Get 5 different random integers
           for (int i = 0; i < 5; i++)
           {
             int indx =   random.Next(0, lastIndex);

             while (quizQuestionIndexes.Contains(indx))
             {
                 indx = random.Next(0, lastIndex);
             }
               quizQuestionIndexes.Add(indx);
           }


           quiz.Questions = quizQuestionIndexes.Select(index => questions[index]).ToList();
           
           Insert(quiz);
           UnitOfWork.Commit();

           return quiz;

       }

       /// <summary>
       /// Checks if quiz completion status needs to be changed.
       /// </summary>
       /// <param name="quiz"></param>
       /// returns true if quiz state has changed.
       /// <returns></returns>
       private static bool checkCompletionStatus(ref Quiz quiz)
       {

           if (quiz.Completed)
               return false;

           if (quiz.Answers.Count == quiz.Questions.Count)
               quiz.Completed = true;

           return true;

       }

       
       

    }
}
