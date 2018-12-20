using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using PolyclinicCourseProject.Models;

namespace PolyclinicCourseProject.Controllers
{
    public class DoctorsTimetableController : Controller
    {
        private readonly DoctorsTimetableDAO timetableDAO = new DoctorsTimetableDAO();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Patient
        public ActionResult Index()
        {
            var timetable = timetableDAO.GetTimetable();
            string query = "select d.* from doctor d inner join doctors_timetable dt on d.Doctor_id = dt.id_doctor ";
            string query1 = "select spec.Specialty_name from list_of_specialty spec inner join doctor d on d.id_specialty = spec.Specialty_id inner join doctors_timetable dt on dt.id_doctor = d.Doctor_id";

            List<doctor> doctors = new List<doctor>();
            using (PolyclinicEntities1 ctx = new PolyclinicEntities1())
            {
                doctors.AddRange(ctx.Database.SqlQuery<doctor>(query).ToList());
            }
            List<FIO> fio = new List<FIO>();
            foreach (doctor el in doctors)
            {
                fio.Add(new FIO(el.Surname, el.Name, el.Patronymic));
            }
            ViewData["Fio"] = fio;

            List<string> specialties = new List<string>();
            using (PolyclinicEntities1 ctx = new PolyclinicEntities1())
            {
                specialties.AddRange(ctx.Database.SqlQuery<string>(query1).ToList());
            }

            int j = 0;
            foreach (doctors_timetable el in timetable)
            {
                el.Specialty = specialties.ElementAt(j);
                j++;
            }
            
            return View(timetable);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            doctors_timetable timetable = new doctors_timetable();
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                //timetable.ListDoctor = db.doctor.ToList<doctor>();
                string doctorFIO = string.Format("select * from doctor");
                List<doctor> dlist = db.Database.SqlQuery<doctor>(doctorFIO).ToList();
                foreach (doctor el in dlist)
                {
                    el.FIO = el.Surname + " " + el.Name + " " + el.Patronymic;
                }
                ViewData["doctorFIO"] = dlist;
            }

            return View(timetable);
        }

        [HttpPost]
        public ActionResult Create(doctors_timetable model)
        {
            try
            {
                timetableDAO.CreateTimetable(model);
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
        public ActionResult Edit(int id = 0)
        {
            doctors_timetable timetable = new doctors_timetable();
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                timetable = db.doctors_timetable.Where(x => x.Timetable_id == id).FirstOrDefault();
                string docFIO = string.Format("select * from doctor");
                List<doctor> doclist = db.Database.SqlQuery<doctor>(docFIO).ToList();
                foreach (doctor el in doclist)
                {
                    el.FIO = el.Surname + " " + el.Name + " " + el.Patronymic;
                }
                ViewData["docFIO"] = doclist;
            }

            return View(timetable);
        }

        [HttpPost]
        public ActionResult Edit(doctors_timetable model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                timetableDAO.EditTimetable(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return RedirectToAction("Index");
        }
    }
}
