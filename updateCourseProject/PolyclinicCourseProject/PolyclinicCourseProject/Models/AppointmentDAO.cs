using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<appointment> GetAppointment(int id_patient)
        {
            List<appointment> appointments = new List<appointment>();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM appointment where id_patient = @P0";
                    appointments.AddRange(ctx.Database.SqlQuery<appointment>(query,id_patient).ToList());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return appointments;
        }

        public void CreateAppointment(appointment model)
        { 
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "INSERT INTO appointment (id_doctor, id_patient, Info_about_appointment, id_diagnosis, Theraphy, Data) VALUES(@P0, @P1, @P2, @P3, @P4, @P5)";
                    List<object> parameterList = new List<object>{
                        model.id_doctor,
                        model.id_patient,
                        model.Info_about_appointment,
                        model.id_diagnosis,
                        model.Theraphy,
                        model.Data
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные успешно добавлены в appointment");

                    making_appointment model1 = new making_appointment();
                    string query1 = string.Format("update making_appointment set Status='Finished' where Record_id={0}", model.idapp);
                    List<object> parameterList1 = new List<object>{
                        model1.Record_id,
                        model1.Status
                    };
                    object[] parameters1 = parameterList.ToArray();
                    int result1 = ctx.Database.ExecuteSqlCommand(query1, parameters1);
                    logger.Info("Данные в making_appointment успешно изменены");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }

        public appointment DetailsAppointment(int Appointment_id)
        {
            appointment appointment = new appointment();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM appointment where Appointment_id = @P0";
                    appointment = ctx.Database.SqlQuery<appointment>(query, Appointment_id).ToList().First();
                }
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return appointment;
        }
    }
}