using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorDAO doctorDAO = new DoctorDAO();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Patient
        public ActionResult Index()
        {
            var doctor = doctorDAO.GetDoctor();
            List<string> specialties = new List<string>();
            using (PolyclinicEntities1 ctx = new PolyclinicEntities1())
            {
                string query1 = "select spec.Specialty_name from list_of_specialty spec inner join doctor d on d.id_specialty = spec.Specialty_id";
                specialties.AddRange(ctx.Database.SqlQuery<string>(query1).ToList());
            }

            int j = 0;
            foreach (doctor el in doctor)
            {
                el.Specialty = specialties.ElementAt(j);
                j++;
            }

            return View(doctor);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            doctor doctor = new doctor();
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
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
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return RedirectToAction("Index", "Home");
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            doctor doctor = new doctor();
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                if (id != 0)
                    doctor = db.doctor.Where(x => x.Doctor_id == id).FirstOrDefault();
                doctor.ListSpetialty = db.list_of_specialty.ToList<list_of_specialty>();
            }
            return View(doctor);
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
            {
                logger.Error("Ошибка: ", ex);
            }
            return RedirectToAction("Index");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id, int idSpecialty)
        {
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                string specialty = string.Format("select * from list_of_specialty where Specialty_id={0}", idSpecialty);
                List<list_of_specialty> splist = db.Database.SqlQuery<list_of_specialty>(specialty).ToList();
                ViewData["specialty"] = splist;
            }
            var doctor = doctorDAO.DetailsDoctor(id);
            return View(doctor);
        }
    }
}