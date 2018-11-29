using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorDAO doctorDAO = new DoctorDAO();

        // GET: Patient
        public ActionResult Index()
        {
            var doctor = doctorDAO.GetDoctor();
            return View(doctor);
        }

        // Create
        [HttpGet]
        public ActionResult Create(/*int id = 0*/)
        {
            doctor doctor = new doctor();
            using (PolyclinicEntities3 db = new PolyclinicEntities3())
            {
               /* if (id != 0)
                    doctor = db.doctor.Where(x => x.Doctor_id == id).FirstOrDefault();*/
                doctor.ListSpetialty = db.list_of_specialty.ToList<list_of_specialty>();
            }

            return View(doctor);
        }

        [HttpPost]
        public ActionResult Create(doctor model)
        {
            try
            {
                doctorDAO.CreateDoctor(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            doctor doctor = new doctor();
            using (PolyclinicEntities3 db = new PolyclinicEntities3())
            {
                if (id != 0)
                     doctor = db.doctor.Where(x => x.Doctor_id == id).FirstOrDefault();
                doctor.ListSpetialty = db.list_of_specialty.ToList<list_of_specialty>();
            }

            return View(doctor);
            //return View();
        }

        [HttpPost]
        public ActionResult Edit(doctor model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                doctorDAO.EditDoctor(model, id);
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
            var doctor = doctorDAO.DetailsDoctor(id);
            return View(doctor);
        }
    }
}