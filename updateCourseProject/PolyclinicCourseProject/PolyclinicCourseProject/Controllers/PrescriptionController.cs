using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
        
        public ActionResult Index()
        {
            var prescription = prescriptionDAO.GetPrescription();
            return View(prescription);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(prescription model)
        {
            try
            {
                prescriptionDAO.CreatePrescription(model);
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
            var prescription = prescriptionDAO.DetailsPrescription(id);
            return View(prescription);
        }
    }
}