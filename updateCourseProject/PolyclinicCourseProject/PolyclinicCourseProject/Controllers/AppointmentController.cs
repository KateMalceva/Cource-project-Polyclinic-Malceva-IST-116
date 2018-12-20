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
            string query= string.Format("select d.* from doctor d inner join appointment dt on d.Doctor_id = dt.id_doctor where dt.id_patient={0}", id);
            List<doctor> doctors = new List<doctor>();
            using (PolyclinicEntities1 ctx = new PolyclinicEntities1())
            {
                doctors.AddRange(ctx.Database.SqlQuery<doctor>(query).ToList());
            }
            List<FIO2> fio = new List<FIO2>();
            foreach (doctor el in doctors)
            {
                fio.Add(new FIO2(el.Surname, el.Name, el.Patronymic));
            }
            ViewData["FIO"] = fio;

            string pquery = string.Format("select p.* from patient p inner join appointment dt on p.Patient_id = dt.id_patient where dt.id_patient={0}", id);
            List<patient> patients = new List<patient>();
            using (PolyclinicEntities1 ctx = new PolyclinicEntities1())
            {
                patients.AddRange(ctx.Database.SqlQuery<patient>(pquery).ToList());
            }
            List<pFIO> pfio = new List<pFIO>();
            foreach (patient el in patients)
            { 
                pfio.Add(new pFIO(el.Surname, el.Name, el.Patronymic));
            }
            ViewData["pFIO"] = pfio;

            string squery = string.Format("select ds.Diagnose_name from list_of_diagnoses ds inner join appointment dt on ds.List_diagnoses_id = dt.id_diagnosis where dt.id_patient={0}", id);
            List<string> diagnoses = new List<string>();
            using (PolyclinicEntities1 ctx = new PolyclinicEntities1())
            {
                diagnoses.AddRange(ctx.Database.SqlQuery<string>(squery).ToList());
            }

            int k = 0;
            foreach (appointment el in appointment)
            {
                el.Diagnose = diagnoses.ElementAt(k);
                k++;
            }

            return View(appointment);
        }

        // Create
        [HttpGet]
        public ActionResult Create(int id, int idapp)
        {
            //ViewData["appointmentID"] = idapp;
            string userId = User.Identity.GetUserId();
            appointment appointment = new appointment();
            //making_appointment making = new making_appointment();
            using (Entities ent = new Entities())
            {
                var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;

                using (PolyclinicEntities1 db = new PolyclinicEntities1())
                {
                    var doctor = db.doctor.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Doctor_id;
                    appointment.ListDiagnoses = db.list_of_diagnoses.ToList<list_of_diagnoses>();
                    appointment.id_doctor = doctor;
                    appointment.id_patient = id;
                    appointment.idapp = idapp;
                }
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
        public ActionResult Details(int id1, int id, int idDoctor, int idDiagnose)
        {
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                string patientFIO = string.Format("select * from patient where Patient_id={0}", id1);
                List<patient> plist = db.Database.SqlQuery<patient>(patientFIO).ToList();
                ViewData["patientFIO"] = plist;
                string doctorFIO = string.Format("select * from doctor where Doctor_id={0}", idDoctor);
                List<doctor> dlist = db.Database.SqlQuery<doctor>(doctorFIO).ToList();
                ViewData["doctorFIO"] = dlist;
                string diagnose = string.Format("select * from list_of_diagnoses where List_diagnoses_id={0}", idDiagnose);
                List<list_of_diagnoses> diagnoselist = db.Database.SqlQuery<list_of_diagnoses>(diagnose).ToList();
                ViewData["diagnose"] = diagnoselist;
            }
            var appointment = appointmentDAO.DetailsAppointment(id);
            return View(appointment);
        }
    }
}