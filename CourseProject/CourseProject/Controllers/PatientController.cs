using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class PatientController : Controller
    {  
        private readonly PatientDAO patientDAO = new PatientDAO();

        // GET: Patient
        public ActionResult Index()
        {
            var patient = patientDAO.GetPatient();
            return View(patient);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(patient model)
        {
            try
            {
                patientDAO.CreatePatient(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var patient = patientDAO.GetPatient().FirstOrDefault(x => x.Patient_id == id);
            return View(patient);
        }

        [HttpPost]
        public ActionResult Edit(patient model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                patientDAO.EditPatient(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var patient = patientDAO.DetailsPatient(id);
            return View(patient);
        }
    }
}