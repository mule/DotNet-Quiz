using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace DotNetQuizDataAccess.Models
{
    public class Quiz
    {

        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Tuple<string, int>> Answers { get; set; }
     


        public Quiz()
        {
            Questions = new List<Question>();
            Answers = new List<Tuple<string, int>>();
            Completed = false;
        }
        public int GetNextQuestionIndex()
        {
            Contract.Requires(Questions != null);
            Contract.Requires(Answers.Count() <= Questions.Count());

            if (Answers == null || !Answers.Any())
                return 0;

            if (Answers.Count == Questions.Count)
                return -1;


            return Answers.Count - 1;
        }

        public bool Completed { get; set; }


        public bool CheckAnswer(int questionIndx, ICollection<int> answers)
        {
            Contract.Requires(Questions != null);
            Contract.Requires(questionIndx < Questions.Count);

            var question = Questions.ElementAtOrDefault(questionIndx);

            if (question != null)
            {
                foreach (int answer in answers)
                {
                    Answers.Add(new Tuple<string, int>(question.Id,answer));
                }

                UnitOfWork.Commit();


                var correctAnswers = question.AnswerOptions.Where(ao => ao.Item3 == true);



                if (answers.Any(answer => !correctAnswers.Select(ca => ca.Item1).Contains(answer)))
                {
                    return false;
                }
                return true;
            }
            else
            {
                //TODO: Log an empty question
                return false;

            }
        }
    }
}