using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetQuiz.Areas.Admin.ViewModels;
using DotNetQuizDataAccess;
using DotNetQuizDataAccess.Models;
using Telerik.Web.Mvc;

namespace DotNetQuiz.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        //
        // GET: /Admin/QuestionAdmin/

        public ActionResult Index()
        {
            var vm = new QuestionsListViewModel();
            vm.LoadData();
            return View(vm);
        }


        [GridAction]
        public ActionResult AjaxBinding()
        {
            var vm = new QuestionsListViewModel();
           vm.LoadData();

            return View(vm);

        }

        //
        // GET: /Admin/QuestionAdmin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/QuestionAdmin/Create

        public ActionResult Create()
        {
            return View( new QuestionEditViewModel());
        } 

        //
        // POST: /Admin/QuestionAdmin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return View();


            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult CreateQuestion(QuestionEditViewModel vm)
        {
            List<string> messages = new List<string>();
            var val = validateQuestion(vm);

            vm.Id = null;
            try
            {
    
                if (val.Item1 == true)
                {
                    var question = CreateQuestionFromViewModel(vm);


                    QuestionManager.Insert(question);
                    UnitOfWork.Commit();
                   
                    messages.Add("Question created succesfully.");
                }
                else
                {
                    messages.Add(val.Item2);
                }

            }
            catch (Exception)
            {
                //TODO: Add logging here
                throw;
            }

          
            return Json(val);

        }
        
        //
        // GET: /Admin/QuestionAdmin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/QuestionAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/QuestionAdmin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/QuestionAdmin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private Tuple<bool,string> validateQuestion(QuestionEditViewModel vm)
        {
            if(String.IsNullOrWhiteSpace(vm.QuestionText))
                return new Tuple<bool, string>(false,"Question text is empty");

            if(vm.AnswerOptions.Count<2)
                return new Tuple<bool, string>(false,"You need 2 or more answer options");

            if(vm.AnswerOptions.Where(ans=>ans.Correct==true).Count() ==0)
                return new Tuple<bool, string>(false, "At least one answer needs to be correct");

            if(vm.AnswerOptions.Where(ans=>String.IsNullOrWhiteSpace(ans.AnswerText)==true).Count()>0)
                return new Tuple<bool, string>(false,"Question cannot contain empty answers");


            return  new Tuple<bool, string>(true,"Question data validated correctly");



        }

        private Question CreateQuestionFromViewModel(QuestionEditViewModel vm)
        {

            Question q = new Question() {QuestionText = vm.QuestionText, Id = vm.Id, QuestionAnswerType =  vm.AnswerType};

                int id = 1;
            q.AnswerOptions = new List<Tuple<int, string, bool>>();

                foreach (AnswerOptionViewModel answerOptionViewModel in vm.AnswerOptions)
                {
                    
                    q.AnswerOptions.Add(new Tuple<int, string, bool>(id,answerOptionViewModel.AnswerText,answerOptionViewModel.Correct));
                    id++;
                }


                return q;


                
            }



        


    }
}
