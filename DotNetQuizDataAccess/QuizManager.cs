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

            return _repository.Single(u => u.Id == id);

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
           var quiz = new Quiz() {StartTime = DateTime.Now};
           var questions = QuestionManager.GetAll().ToArray();

           int lastIndex = questions.Count() - 1;

           var random = new Random(DateTime.Now.Millisecond);



           return quiz;

       }
    }
}
