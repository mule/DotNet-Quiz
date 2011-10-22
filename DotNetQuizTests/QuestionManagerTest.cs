using System.Collections.Generic;
using System.Diagnostics;
using DotNetQuizDataAccess;
using DotNetQuizDataAccess.Interface;
using StructureMap;
using Xunit;
using System;
using DotNetQuizDataAccess.Models;

namespace DotNetQuizTests
{


    /// <summary>
    ///This is a test class for QuestionManagerTest and is intended
    ///to contain all QuestionManagerTest Unit Tests
    ///</summary>

    public class QuestionManagerTest
    {





        /// <summary>
        ///A test for Insert
        ///</summary>
        [Fact]
        public void InsertTest()
        {

            setupDb();


            var questionStr =
@"This is a test";

            var answer1 = new Tuple<int, string, bool>(1, "Replace the GridView control with a ListView control.", false);
            var answer2 = new Tuple<int, string, bool>(2, "Set the ClientIDMode attribute to Predictable in the web.config file.", false);
            var answer3 = new Tuple<int, string, bool>(3, "Set the ClientIDRowSuffix attribute of each unique GridView control to a different value.", false);
            var answer4 = new Tuple<int, string, bool>(4,
                                                 "Set the @ OutputCache directive's VaryByControl attribute to the ID of the GridView control.", true);

            Question entity = new Question()
                                  {
                                      QuestionAnswerType = Question.AnswerType.MultipleChoice,
                                      QuestionText = "TestQuestion",
                                      AnswerOptions =
                                          new List<Tuple<int, string, bool>>() { answer1, answer2, answer3, answer4 }
                                  };
            QuestionManager.Insert(entity);

            UnitOfWork.Commit();



        }

        [Fact]
        public void GetAllTest()
        {
            setupDb();


            var questions = QuestionManager.GetAll();


            foreach (Question question in questions)
            {
                Trace.WriteLine(question.ToString());
            }

        }


        private void setupDb()
        {

            // Initialize the Repository factory.
            ObjectFactory.Initialize(
                x =>
                {
                    x.For<IUnitOfWorkFactory>().Use<RavenUnitOfWorkFactory>();
                    x.For(typeof(IRepository<>)).Use(typeof(RavenRepository<>));
                }
            );

            // RavenDB initialization method.
            RavenUnitOfWorkFactory.SetObjectContext(
                () =>
                {
                    var documentStore =
new Raven.Client.Document.DocumentStore { Url = "http://localhost:8080", DefaultDatabase = "DotNetQuizUnitTest" };
                    documentStore.Conventions.IdentityPartsSeparator = "-";
                    documentStore.Initialize();

                    return documentStore;
                }
            );



        }


    }
}
