using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentDAO appointmentDAO = new AppointmentDAO();

        public ActionResult Index(int id)
        {
            var appointment = appointmentDAO.GetAppointment(id);
            return View(appointment);
        }

        // Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            string userId = User.Identity.GetUserId();
            appointment appointment = new appointment();
            Entities ent = new Entities();
            var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;

            using (PolyclinicEntities db = new PolyclinicEntities())
            {
                var doctor = db.doctor.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Doctor_id;
                appointment.ListDiagnoses = db.list_of_diagnoses.ToList<list_of_diagnoses>();
                appointment.id_doctor = doctor;
                appointment.id_patient = id;
            }
            return View(appointment);
        }

        [HttpPost]
        public ActionResult Create(appointment model)
        {
            try
            {
                appointmentDAO.CreateAppointment(model);
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index","Patient");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var appointment = appointmentDAO.DetailsAppointment(id);
            return View(appointment);
        }
    }
}