using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyclinicCourseProject.Models
{
    public class PrescriptionDAO
    {
        public List<prescription> GetPrescription()
        {
            List<prescription> prescriptions = new List<prescription>();
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "SELECT * FROM prescription";
                    prescriptions.AddRange(ctx.Database.SqlQuery<prescription>(query).ToList());
                }
            }
            catch (Exception ex)
            { }
            return prescriptions;
        }

        public void CreatePrescription(prescription model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "INSERT INTO prescription (id_doctor, id_patient, Date, Content) VALUES(@P0, @P1, @P2, @P3)";
                    List<object> parameterList = new List<object>{
                        model.id_doctor,
                        model.id_patient,
                        model.Date,
                        model.Content
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                }
            }
            catch (Exception ex)
            { }
        }

        public prescription DetailsPrescription(int Prescription_id)
        {
            prescription prescription= new prescription();
            using (var ctx = new PolyclinicEntities())
            {
                string query = "SELECT * FROM prescription where Prescription_id = @P0";
                prescription = ctx.Database.SqlQuery<prescription>(query, Prescription_id).ToList().First();
            }
            return prescription;
        }
    }
}