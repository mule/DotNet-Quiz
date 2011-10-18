using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetQuizDataAccess.Interface;
using DotNetQuizDataAccess.Models;
using StructureMap;

namespace DotNetQuizDataAccess
{
    public class QuestionManager
    {

        private static IRepository<Question> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Question>>();
            }

        }

        public static Question Load(int id)
        {

            return _repository.Single(u => u.QuestionId == id);

        }

        public static List<Question> GetAll()
        {
            List<Question> list = new List<Question>();

            // Fetch all entities from the repository.
            list = _repository.GetAll().ToList();

            return list;
        }

        public static void Insert(Question entity)
        {
            // Add the new entity to the repository.
            _repository.Add(entity);
         
        }

        public static void Delete(Question entity)
        {
            // Add any custom business rules.
            _repository.Delete(entity);
        }




    }
}
