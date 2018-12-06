using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolyclinicCourseProject.Controllers
{
    public class MakingAppointmentController : Controller
    {
        // GET: MakingAppointment
        //public ActionResult Index()
        //{
        //    return View();
        //}

        PolyclinicEntities db = new PolyclinicEntities();

        public ActionResult Index()
        {
            int selectedIndex = 1;
            SelectList specialties = new SelectList(db.list_of_specialty, "Specialty_id", "Specialty_name", selectedIndex);
            ViewBag.list_of_specialty = specialties;
            SelectList doctors = new SelectList(db.doctor.Where(c => c.id_specialty == selectedIndex), "Doctor_id", "Surname");
            ViewBag.doctor = doctors;
            return View();
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.doctor.Where(c => c.id_specialty == id).ToList());
        }
    }
}