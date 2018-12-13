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
        private readonly PatientDAO patientDAO = new PatientDAO();

        public ActionResult Index(int id)
        {
            var prescription = prescriptionDAO.GetPrescription(id);
            ViewData["patientID"] = id;

            string query = string.Format("select d.* from doctor d inner join prescription dt on d.Doctor_id = dt.id_doctor where dt.id_patient={0}", id);
            List<doctor> doctors = new List<doctor>();
            using (PolyclinicEntities ctx = new PolyclinicEntities())
            {
                doctors.AddRange(ctx.Database.SqlQuery<doctor>(query).ToList());
            }
            List<FIO2> fio = new List<FIO2>();
            foreach (doctor el in doctors)
            {
                fio.Add(new FIO2(el.Surname, el.Name, el.Patronymic));
            }
            ViewData["FIO"] = fio;

            string pquery = string.Format("select p.* from patient p inner join prescription dt on p.Patient_id = dt.id_patient where dt.id_patient={0}", id);
            List<patient> patients = new List<patient>();
            using (PolyclinicEntities ctx = new PolyclinicEntities())
            {
                patients.AddRange(ctx.Database.SqlQuery<patient>(pquery).ToList());
            }
            List<pFIO> pfio = new List<pFIO>();
            foreach (patient el in patients)
            {
                pfio.Add(new pFIO(el.Surname, el.Name, el.Patronymic));
            }
            ViewData["pFIO"] = pfio;

            return View(prescription);
        }

        public ActionResult GetUserPrescription()
        {
            string userId = User.Identity.GetUserId();
            patient patient1 = new patient();
            Entities ent = new Entities();
            var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;
            using (PolyclinicEntities db = new PolyclinicEntities())
            {
                var patientId = db.patient.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Patient_id;
                var prescription = prescriptionDAO.GetPrescription(patientId);

                string query = string.Format("select d.* from doctor d inner join prescription dt on d.Doctor_id = dt.id_doctor where dt.id_patient={0}", patientId);
                List<doctor> doctors = new List<doctor>();
                using (PolyclinicEntities ctx = new PolyclinicEntities())
                {
                    doctors.AddRange(ctx.Database.SqlQuery<doctor>(query).ToList());
                }
                List<FIO2> fio = new List<FIO2>();
                foreach (doctor el in doctors)
                {
                    fio.Add(new FIO2(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["FIO"] = fio;

                string pquery = string.Format("select p.* from patient p inner join prescription dt on p.Patient_id = dt.id_patient where dt.id_patient={0}", patientId);
                List<patient> patients = new List<patient>();
                using (PolyclinicEntities ctx = new PolyclinicEntities())
                {
                    patients.AddRange(ctx.Database.SqlQuery<patient>(pquery).ToList());
                }
                List<pFIO> pfio = new List<pFIO>();
                foreach (patient el in patients)
                {
                    pfio.Add(new pFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["pFIO"] = pfio;
                return View(prescription);
            }
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
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index", "Patient");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id1, int id, int idDoctor)
        {
            using (PolyclinicEntities db = new PolyclinicEntities())
            {
                string patientFIO = string.Format("select * from patient where Patient_id={0}", id1);
                List<patient> plist = db.Database.SqlQuery<patient>(patientFIO).ToList();
                ViewData["patientFIO"] = plist;
                string doctorFIO = string.Format("select * from doctor where Doctor_id={0}", idDoctor);
                List<doctor> dlist = db.Database.SqlQuery<doctor>(doctorFIO).ToList();
                ViewData["doctorFIO"] = dlist;
            }
            var prescription = prescriptionDAO.DetailsPrescription(id);
            return View(prescription);
        }
    }
}