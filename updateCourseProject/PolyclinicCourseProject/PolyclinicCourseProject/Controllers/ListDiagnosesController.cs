using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class ListDiagnosesController : Controller
    {
        private readonly ListDiagnosesDAO diagnosesDAO = new ListDiagnosesDAO();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET
        public ActionResult Index()
        {
            var diagnosis = diagnosesDAO.GetDiagnoses();
            return View(diagnosis);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(list_of_diagnoses model)
        {
            try
            {
                diagnosesDAO.CreateDiagnosis(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var diagnosis = diagnosesDAO.GetDiagnoses().FirstOrDefault(x => x.List_diagnoses_id == id);
            return View(diagnosis);
        }

        [HttpPost]
        public ActionResult Edit(list_of_diagnoses model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                diagnosesDAO.EditDiagnosis(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return RedirectToAction("Index");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var diagnosis = diagnosesDAO.DetailsDiagnosis(id);
            return View(diagnosis);
        }
    }
}