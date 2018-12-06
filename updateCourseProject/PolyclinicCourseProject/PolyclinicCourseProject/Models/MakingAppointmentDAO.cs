using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyclinicCourseProject.Models
{
    public class MakingAppointmentDAO
    {
        public List<making_appointment> GetAppointment()
        {
            List<making_appointment> making_Appointments = new List<making_appointment>();
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "SELECT * FROM making_appointment";
                    making_Appointments.AddRange(ctx.Database.SqlQuery<making_appointment>(query).ToList());
                }
            }
            catch (Exception ex)
            { }
            return making_Appointments;
        }

        public void CreateAppointment(making_appointment model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities())
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

                }
            }
            catch (Exception ex)
            { }
        }

        public void EditAppointment(making_appointment model, int Record_id)
        {
            using (var ctx = new PolyclinicEntities())
            {
                string query = "update (making_appointment set Date=@P1, Time=@P2, id_doctor=@P3, id_patient=@P4, Status=@P5 where Record_id=@P0";
                List<object> parameterList = new List<object>{
                        Record_id,
                        model.Date,
                        model.Time,
                        model.id_doctor,
                        model.id_patient,
                        model.Status
                    };
                object[] parameters = parameterList.ToArray();
                int result = ctx.Database.ExecuteSqlCommand(query, parameters);
            }
        }


        public making_appointment DetailsAppointment(int Record_id)
        {
            making_appointment making = new making_appointment();
            using (var ctx = new PolyclinicEntities())
            {
                string query = "SELECT * FROM making_appointment where Record_id = @P0";
                making = ctx.Database.SqlQuery<making_appointment>(query, Record_id).ToList().First();
            }
            return making;
        }
    }
}