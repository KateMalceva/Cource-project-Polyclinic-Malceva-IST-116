using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentDAO appointmentDAO = new AppointmentDAO();

        public ActionResult Index()
        {
            var appointment = appointmentDAO.GetAppointment();
            return View(appointment);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            appointment appointment = new appointment();
            using (PolyclinicEntities db = new PolyclinicEntities())
            {
                appointment.ListDiagnoses = db.list_of_diagnoses.ToList<list_of_diagnoses>();
            }
            return View(appointment);
        }

        [HttpPost]
        public ActionResult Create(appointment model)
        {
            try
            {
                appointmentDAO.CreateAppointment(model);
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
            var appointment = appointmentDAO.DetailsAppointment(id);
            return View(appointment);
        }
    }
}