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
        public ICollection<Tuple<string, int>> Answers { get; private set; }
        public ICollection<string> CorrectlyAnsweredQuestions { get; private set; } 
        public int NextQuestionIndex { get; private set; }
        public bool Completed { get;  private set; }

     


        public Quiz()
        {
            Questions = new List<Question>();
            Answers = new List<Tuple<string, int>>();
            CorrectlyAnsweredQuestions = new List<string>();
            Completed = false;
            NextQuestionIndex = 0;
        }


      

        public bool CheckAnswer(int questionIndx, ICollection<int> answers)
        {
            Contract.Requires(Questions != null);
            Contract.Requires(questionIndx < Questions.Count);
            

            var question = Questions.ElementAtOrDefault(questionIndx);

            if (question != null)
            {

                if (!Completed)
                {
                    foreach (int answer in answers)
                    {
                        Answers.Add(new Tuple<string, int>(question.Id, answer));
                    }


                    if (NextQuestionIndex == Questions.Count - 1)
                    {
                        Completed = true;
                    }
                    else
                    {
                        NextQuestionIndex++;
                    }

                                    var correctAnswers = question.AnswerOptions.Where(ao => ao.Item3 == true);
                if (answers.Any(answer => !correctAnswers.Select(ca => ca.Item1).Contains(answer)))
                {
                    return false;
                }

                    CorrectlyAnsweredQuestions.Add(question.Id);
                

                    UnitOfWork.Commit();
                }

            }
            else
            {
                //TODO: Log an empty question
                return false;

            }

            return false; 
        }

     
    }
}