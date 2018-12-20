using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
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
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("DetailsUser","Patient");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("DetailsUser", "Patient");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var patient = patientDAO.DetailsPatient(id);
            return View(patient);
        }

        //Details for user
        [HttpGet]
        public ActionResult DetailsUser()
        {
            string userId = User.Identity.GetUserId();
            patient patient1 = new patient();
            Entities ent = new Entities();
            var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                var patientId = db.patient.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Patient_id;
                var patient = patientDAO.DetailsPatient(patientId);
                return View(patient);
            }
        }
    }
}