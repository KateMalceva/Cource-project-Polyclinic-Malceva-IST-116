using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    public class DoctorsTimetableController : Controller
    {
        private readonly DoctorsTimetableDAO timetableDAO = new DoctorsTimetableDAO();

        // GET: Patient
        public ActionResult Index()
        {
            var timetable = timetableDAO.GetTimetable();
            string query = "select d.* from doctor d inner join docrors_timetable dt on d.Doctor_id = dt.id_doctor ";
            string query1 = "select spec.Spetialty_name from list_of_specialty spec inner join doctor d on d.id_specialty = spec.Specialty_id inner join docrors_timetable dt on dt.id_doctor = d.Doctor_id";

            List<doctor> doctors = new List<doctor>();
            using (PolyclinicEntities3 ctx = new PolyclinicEntities3())
            {
                doctors.AddRange(ctx.Database.SqlQuery<doctor>(query).ToList());
            }
            List<FIO> fio = new List<FIO>();
            foreach (doctor el in doctors)
            {
                fio.Add(new FIO(el.Surname, el.Name,  el.Patronymic));
            }
            ViewData["Fio"] = fio;

            List<string> specialties = new List<string>();
            using (PolyclinicEntities3 ctx = new PolyclinicEntities3())
            {
                specialties.AddRange(ctx.Database.SqlQuery<string>(query1).ToList());
            }
           
            int j = 0;
            foreach (docrors_timetable el in timetable)
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
            docrors_timetable timetable = new docrors_timetable();
            using (PolyclinicEntities3 db = new PolyclinicEntities3())
            {
                timetable.ListDoctor = db.doctor.ToList<doctor>();
            }

            return View(timetable);
        }

        [HttpPost]
        public ActionResult Create(docrors_timetable model)
        {
            try
            {
                timetableDAO.CreateTimetable(model);
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
            docrors_timetable timetable = new docrors_timetable();
            using (PolyclinicEntities3 db = new PolyclinicEntities3())
            {
                if (id != 0)
                    timetable = db.docrors_timetable.Where(x => x.Timetable_id == id).FirstOrDefault();
                timetable.ListDoctor = db.doctor.ToList<doctor>();
            }

            return View(timetable);
        }

        [HttpPost]
        public ActionResult Edit(docrors_timetable model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                timetableDAO.EditTimetable(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }
    }
}