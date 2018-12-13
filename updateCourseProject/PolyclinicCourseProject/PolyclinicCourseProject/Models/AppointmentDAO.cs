using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace PolyclinicCourseProject.Models
{
    public class FIO2
    {
        public string surname;
        public string name;
        public string patronymic;
        public FIO2(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public class pFIO
    {
        public string surname;
        public string name;
        public string patronymic;
        public pFIO(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public class AppointmentDAO
    {
        public List<appointment> GetAppointment(int id_patient)
        {
            List<appointment> appointments = new List<appointment>();
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "SELECT * FROM appointment where id_patient = @P0";
                    appointments.AddRange(ctx.Database.SqlQuery<appointment>(query,id_patient).ToList());
                }
            }
            catch (Exception ex)
            { }
            return appointments;
        }

        public void CreateAppointment(appointment model)
        { 
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "INSERT INTO appointment (id_doctor, id_patient, Info_about_appointment, id_diagnosis, Theraphy) VALUES(@P0, @P1, @P2, @P3, @P4)";
                    List<object> parameterList = new List<object>{
                        model.id_doctor,
                        model.id_patient,
                        model.Info_about_appointment,
                        model.id_diagnosis,
                        model.Theraphy
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                }
            }
            catch (Exception ex)
            { }
        }

        public appointment DetailsAppointment(/*int id_patient*/int Appointment_id)
        {
            appointment appointment = new appointment();
            using (var ctx = new PolyclinicEntities())
            {
                string query = "SELECT * FROM appointment where Appointment_id = @P0";
                appointment = ctx.Database.SqlQuery<appointment>(query, /*id_patient*/Appointment_id).ToList().First();
            }
            return appointment;
        }
    }
}