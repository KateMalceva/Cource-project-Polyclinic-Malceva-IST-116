using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyclinicCourseProject.Models
{
    public class docFIO
    {
        public string surname;
        public string name;
        public string patronymic;
        public docFIO(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public class doctorFIO
    {
        public string surname;
        public string name;
        public string patronymic;
        public doctorFIO(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public class patFIO
    {
        public string surname;
        public string name;
        public string patronymic;
        public patFIO(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public class patientFIO
    {
        public string surname;
        public string name;
        public string patronymic;
        public patientFIO(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public class MakingAppointmentDAO
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<making_appointment> GetAppointment()
        {
            List<making_appointment> making_Appointments = new List<making_appointment>();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM making_appointment";
                    making_Appointments.AddRange(ctx.Database.SqlQuery<making_appointment>(query).ToList());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return making_Appointments;
        }

        public List<making_appointment> GetAppointmentUser(int id_patient)
        {
            List<making_appointment> making_Appointments = new List<making_appointment>();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM making_appointment where id_patient = @P0";
                    making_Appointments.AddRange(ctx.Database.SqlQuery<making_appointment>(query, id_patient).ToList());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return making_Appointments;
        }

        public List<making_appointment> GetAppointmentDoctor(int id_doctor)
        {
            List<making_appointment> making_Appointments = new List<making_appointment>();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM making_appointment where id_doctor = @P0";
                    making_Appointments.AddRange(ctx.Database.SqlQuery<making_appointment>(query, id_doctor).ToList());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return making_Appointments;
        }

        public void CreateAppointment(making_appointment model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "INSERT INTO making_appointment (Date, Time, id_doctor, id_patient, Status) VALUES(@P0, @P1, @P2, @P3, @P4)";
                    List<object> parameterList = new List<object>{
                        model.Date,
                        model.Time,
                        model.id_doctor,
                        model.id_patient,
                        model.Status
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные успешно добавлены в making_appointment");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }

        public void EditAppointment(making_appointment model, int Record_id)
        {
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "update making_appointment set Status='Cancelled' where Record_id=@P0";
                    List<object> parameterList = new List<object>{
                        Record_id,
                        model.Status
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные в making_appointment успешно изменены");
                }
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }

        public making_appointment DetailsAppointment(int Record_id)
        {
            making_appointment making = new making_appointment();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM making_appointment where Record_id = @P0";
                    making = ctx.Database.SqlQuery<making_appointment>(query, Record_id).ToList().First();
                }
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return making;
        }
    }
}