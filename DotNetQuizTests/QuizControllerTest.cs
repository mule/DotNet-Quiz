using System.Diagnostics;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using DotNetQuiz.Controllers;
using DotNetQuizDataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;

namespace DotNetQuizTests
{
    
    
    /// <summary>
    ///This is a test class for QuizControllerTest and is intended
    ///to contain all QuizControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QuizControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Index
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\projects\\DotNetQuiz\\DotNetQuiz", "/")]
        [UrlToTest("http://localhost:53311/")]
        public void IndexTest()
        {
            QuizController target = new QuizController(); // TODO: Initialize to an appropriate value


            var actual = target.Index() as ViewResult;
            Assert.IsInstanceOfType(actual.Model,typeof(Question));
    
        }

        /// <summary>
        ///A test for Answer
        ///</summary>
        // TODO: Ensure that the UrlToTest atBtribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\projects\\DotNetQuiz\\DotNetQuiz", "/")]
        [UrlToTest("http://localhost:53311/")]
        public void AnswerTest()
        {
            QuizController target = new QuizController();
            int question = 0;
            var answers = new int[] {1};
            string quizId ="quizzes-1";


            var actual = target.Answer(question, answers,quizId) as JsonResult;
            dynamic data = actual.Data;

           Assert.IsTrue(data.correct == false);
            Assert.IsTrue(data.message == "Test message");





        }

        /// <summary>
        ///A test for NextQuestion
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        //[TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("D:\\projects\\DotNet-Quiz\\DotNetQuiz", "/")]
        //[UrlToTest("http://localhost:53311/")]
        //public void NextQuestionTest()
        //{
        //    QuizController target = new QuizController(); 

        //    var actual = target.NextQuestion("Quiz/1") as JsonResult;
           
        //    dynamic data = actual.Data;


        //    Assert.IsTrue(data.AnswerType == Question.AnswerType.MultipleChoice);
     
        //}
    }
}
