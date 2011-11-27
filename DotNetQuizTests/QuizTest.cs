using System.Collections.Generic;
using System.Collections.ObjectModel;
using DotNetQuizDataAccess.Models;

using System;
using System.Linq;
using Xunit;


namespace DotNetQuizTests
{
    
    
    /// <summary>
    ///This is a test class for QuizTest and is intended
    ///to contain all QuizTest Unit Tests
    ///</summary>
    /// 
    public class QuizTest
    {

        private  Quiz testQuiz;

        private void setupTestQuiz()
        {

            testQuiz = new Quiz();
            testQuiz.Id = "Quiz/1";

            var question = new Question
            {
                QuestionText = "Test question",
                Id = "Question/1",
                AnswerOptions = new List<Tuple<int, string, bool>>
                                                       {
                                                           new Tuple<int, string, bool>(0, "test1", false),
                                                           new Tuple<int, string, bool>(1, "test1", true)
                                                       }
            };

            var question2 = new Question
            {
                QuestionText = "Test question",
                Id = "Question/2",
                AnswerOptions = new List<Tuple<int, string, bool>>
                                                       {
                                                           new Tuple<int, string, bool>(0, "test1", false),
                                                           new Tuple<int, string, bool>(1, "test1", true)
                                                       }
            };

            testQuiz.Questions.Add(question);
            testQuiz.Questions.Add(question2);

        }
        // [Fact]
        //public void Test_getting_next_question_from_quiz()
        //{
        //     setupTestQuiz();

        //     var question =  testQuiz.Questions.First();
        //    var actual = testQuiz.GetNextQuestion();

        //    Assert.True(actual.Id == question.Id);


        //}

        ////[Fact]
        //public void Test_quiz_getNextQuestion_should_return_unanswered_question()
        //{
        //    setupTestQuiz();
        //    var answer = new Tuple<string, int> ("Question/1", 1);

        //    testQuiz.Answers = new List<Tuple<string, int>>(){answer};

        //    var actual = testQuiz.GetNextQuestion();

        //    Assert.True(actual.Id == "Question/2");



        //}


        [Fact]
        public void Tests_if_correct_answer_is_found_from_a_question()
        {
            setupTestQuiz();

            Assert.True(testQuiz.CheckAnswer(0, new Collection<int>() {1}));
            Assert.False(testQuiz.CheckAnswer(0, new Collection<int>(){0}));
            Assert.False(testQuiz.CheckAnswer(0, new Collection<int>() {0,1 }));


           

        }


    }
}
