using Microsoft.AspNet.Identity;
using PolyclinicCourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PolyclinicCourseProject.Controllers
{
    public class MakingAppointmentController : Controller
    {
        private readonly MakingAppointmentDAO appointmentDAO = new MakingAppointmentDAO();
        //PolyclinicEntities1 db = new PolyclinicEntities1();

        //Appointments for admin
        public ActionResult Index()
        {
            var appointment = appointmentDAO.GetAppointment();
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                string query = string.Format("select d.* from doctor d inner join making_appointment dt on d.Doctor_id = dt.id_doctor");
                List<doctor> doctors = new List<doctor>();
                doctors.AddRange(db.Database.SqlQuery<doctor>(query).ToList());
                List<docFIO> fio = new List<docFIO>();
                foreach (doctor el in doctors)
                {
                    fio.Add(new docFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["docFIO"] = fio;

                string pquery = string.Format("select p.* from patient p inner join making_appointment dt on p.Patient_id = dt.id_patient");
                List<patient> patients = new List<patient>();
                patients.AddRange(db.Database.SqlQuery<patient>(pquery).ToList());
                List<patFIO> pfio = new List<patFIO>();
                foreach (patient el in patients)
                {
                    pfio.Add(new patFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["patFIO"] = pfio;
            }
            return View(appointment);
        }

        //Appointments for patient
        public ActionResult GetMakingAppointment()
        {
            string userId = User.Identity.GetUserId();
            patient patient1 = new patient();
            Entities ent = new Entities();
            var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                var patientId = db.patient.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Patient_id;
                var mappointment = appointmentDAO.GetAppointmentUser(patientId);

                string query = string.Format("select d.* from doctor d inner join making_appointment dt on d.Doctor_id = dt.id_doctor where dt.id_patient={0}", patientId);
                List<doctor> doctors = new List<doctor>();
                doctors.AddRange(db.Database.SqlQuery<doctor>(query).ToList());
                List<docFIO> fio = new List<docFIO>();
                foreach (doctor el in doctors)
                {
                    fio.Add(new docFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["docFIO"] = fio;

                string pquery = string.Format("select p.* from patient p inner join making_appointment dt on p.Patient_id = dt.id_patient where dt.id_patient={0}", patientId);
                List<patient> patients = new List<patient>();
                patients.AddRange(db.Database.SqlQuery<patient>(pquery).ToList());
                List<patFIO> pfio = new List<patFIO>();
                foreach (patient el in patients)
                {
                    pfio.Add(new patFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["patFIO"] = pfio;
                return View(mappointment);
            }
        }

        //Appointments for doctor
        public ActionResult DoctorGetMakingAppointment()
        {
            string userId = User.Identity.GetUserId();
            doctor doctor = new doctor();
            Entities ent = new Entities();
            var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;
            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                var doctorId = db.doctor.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Doctor_id;
                var mappointment = appointmentDAO.GetAppointmentDoctor(doctorId);
                string query = string.Format("select d.* from doctor d inner join making_appointment dt on d.Doctor_id = dt.id_doctor where dt.id_doctor={0}", doctorId);
                List<doctor> doctors = new List<doctor>();
                doctors.AddRange(db.Database.SqlQuery<doctor>(query).ToList());
                List<docFIO> fio = new List<docFIO>();
                foreach (doctor el in doctors)
                {
                    fio.Add(new docFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["doctorFIO"] = fio;

                string pquery = string.Format("select p.* from patient p inner join making_appointment dt on p.Patient_id = dt.id_patient where dt.id_doctor={0}", doctorId);
                List<patient> patients = new List<patient>();
                patients.AddRange(db.Database.SqlQuery<patient>(pquery).ToList());
                List<patFIO> pfio = new List<patFIO>();
                foreach (patient el in patients)
                {
                    pfio.Add(new patFIO(el.Surname, el.Name, el.Patronymic));
                }
                ViewData["patientFIO"] = pfio;
                return View(mappointment);
            }
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            //string userId = User.Identity.GetUserId();
            making_appointment appointment = new making_appointment();
            //Entities ent = new Entities();
            //var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;

            using (PolyclinicEntities1 db = new PolyclinicEntities1())
            {
                //var idPatient = db.patient.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Patient_id;
                //appointment.id_patient = idPatient;
                //appointment.Status = "Available";

                string doctorFIO = string.Format("select * from doctor");
                List<doctor> dlist = db.Database.SqlQuery<doctor>(doctorFIO).ToList();
                foreach (doctor el in dlist)
                {
                    el.FIO = el.Surname + " " + el.Name + " " + el.Patronymic;
                }
                ViewData["doctorFIO"] = dlist;
            }
            return View(appointment);
        }

        [HttpPost]
        public ActionResult Create(making_appointment model)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                Entities ent = new Entities();
                var userPhoneNumber = ent.AspNetUsers.Find(userId).PhoneNumber;
                using (var ctx = new PolyclinicEntities1())
                {
                    var idPatient = ctx.patient.FirstOrDefault(x => x.Phone_number.ToString() == userPhoneNumber).Patient_id;
                    model.id_patient = idPatient;
                    model.Status = "Available";

                    if (model.Date.DayOfWeek == DayOfWeek.Monday)
                    {
                        var time = ctx.doctors_timetable.FirstOrDefault(x => x.id_doctor == model.id_doctor).Monday;
                        model.Time = time;
                    }
                    else if (model.Date.DayOfWeek == DayOfWeek.Tuesday)
                    {
                        var time = ctx.doctors_timetable.FirstOrDefault(x => x.id_doctor == model.id_doctor).Tuesday;
                        model.Time = time;
                    }
                    else if (model.Date.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        var time = ctx.doctors_timetable.FirstOrDefault(x => x.id_doctor == model.id_doctor).Wednesday;
                        model.Time = time;
                    }
                    else if (model.Date.DayOfWeek == DayOfWeek.Thursday)
                    {
                        var time = ctx.doctors_timetable.FirstOrDefault(x => x.id_doctor == model.id_doctor).Thursday;
                        model.Time = time;
                    }
                    else if (model.Date.DayOfWeek == DayOfWeek.Friday)
                    {
                        var time = ctx.doctors_timetable.FirstOrDefault(x => x.id_doctor == model.id_doctor).Friday;
                        model.Time = time;
                    }
                }
                appointmentDAO.CreateAppointment(model);
                return RedirectToAction("GetMakingAppointment", "MakingAppointment");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("GetMakingAppointment", "MakingAppointment");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var app = appointmentDAO.GetAppointment().FirstOrDefault(x => x.id_patient == id);
            //making_appointment appointment = new making_appointment();
            //using (PolyclinicEntities1 db = new PolyclinicEntities1())
            //{
            //    appointment.Status = "Cancelled";
            //}
            return View(app);
        }

        [HttpPost]
        public ActionResult Edit(making_appointment model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                appointmentDAO.EditAppointment(model, id);
                return RedirectToAction("GetMakingAppointment", "MakingAppointment");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("GetMakingAppointment", "MakingAppointment");
        }

        //[HttpGet]
        //public ActionResult EditPatientNotCome(int id)
        //{
        //    var app = appointmentDAO.GetAppointment().FirstOrDefault(x => x.id_patient == id);
        //    making_appointment appointment = new making_appointment();
        //    using (PolyclinicEntities1 db = new PolyclinicEntities1())
        //    {
        //        appointment.Status = "Not come";
        //    }
        //    return View(app);
        //}

        //[HttpPost]
        //public ActionResult EditPatientNotCome(making_appointment model)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
        //        appointmentDAO.EditAppointmentNotCome(model, id);
        //        return RedirectToAction("DoctorGetMakingAppointment", "MakingAppointment");
        //    }
        //    catch (Exception ex)
        //    { }
        //    return RedirectToAction("DoctorGetMakingAppointment", "MakingAppointment");
        //}
    }
}