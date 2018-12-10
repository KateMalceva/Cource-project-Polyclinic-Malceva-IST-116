using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
        
        public ActionResult Index(int id)
        {
            var prescription = prescriptionDAO.GetPrescription(id);
            return View(prescription);
        }

        // Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            //return View();
            string userId = User.Identity.GetUserId();
            prescription prescription = new prescription();
            Entities ent = new Entities();
            var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;

            using (PolyclinicEntities db = new PolyclinicEntities())
            {
                var doctorId = db.doctor.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Doctor_id;
                prescription.id_doctor = doctorId;
                prescription.id_patient = id;
            }
            return View(prescription);
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