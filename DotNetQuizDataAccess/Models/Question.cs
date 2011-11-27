using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DotNetQuizDataAccess.Models
{
    public class Question
    {
        public enum AnswerType
        {
            SingleChoice = 1,
            MultipleChoice = 2,
            Unknown = 0,

        }



        public string Id { get; set; }
        public AnswerType QuestionAnswerType { get; set; }  
        public string QuestionText { get; set; }
        public List<Tuple<int, string, bool>> AnswerOptions { get; set; }




        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat(@"Question Id: {0} Text: {1} AnswerType: {2}", Id, QuestionText,
                            Enum.GetName(typeof (AnswerType), QuestionAnswerType));

            sb.AppendLine("Answer options:");

            foreach (Tuple<int, string, bool> answerOption in AnswerOptions)
            {
                sb.AppendFormat(@"Id: {0} Text: {1} Correct: {2}", answerOption.Item1, answerOption.Item2,
                                answerOption.Item3);
                sb.AppendLine();

            }

            return sb.ToString();
        }

    }
}