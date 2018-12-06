using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyclinicCourseProject.Models
{
    public class PatientDAO
    {
        public List<patient> GetPatient()
        {
            List<patient> patients = new List<patient>();
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "SELECT * FROM patient";
                    patients.AddRange(ctx.Database.SqlQuery<patient>(query).ToList());
                }
            }
            catch (Exception ex)
            { }
            return patients;
        }

        public void CreatePatient(patient model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities())
                {
                    string query = "INSERT INTO patient (Surname, Name, Patronymic, Date_of_birth, Phone_number, Adress, Insurance_policy) VALUES(@P0, @P1, @P2, @P3, @P4, @P5, @P6)";
                    List<object> parameterList = new List<object>{
                        model.Surname,
                        model.Name,
                        model.Patronymic,
                        model.Date_of_birth,
                        model.Phone_number,
                        model.Adress,
                        model.Insurance_policy,
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);

                }
            }
            catch (Exception ex)
            { }
        }

        public void EditPatient(patient model, int Patient_id)
        {
            using (var ctx = new PolyclinicEntities())
            {
                string query = "update patient set Surname=@P1, Name=@P2, Patronymic=@P3, Date_of_birth=@P4, Phone_number=@P5, Adress=@P6, Insurance_policy=@P7 where Patient_id=@P0";
                List<object> parameterList = new List<object>{
                        Patient_id,
                        model.Surname,
                        model.Name,
                        model.Patronymic,
                        model.Date_of_birth,
                        model.Phone_number,
                        model.Adress,
                        model.Insurance_policy
                    };
                object[] parameters = parameterList.ToArray();
                int result = ctx.Database.ExecuteSqlCommand(query, parameters);
            }
        }


        public patient DetailsPatient(int Patient_id)
        {
            patient patient = new patient();
            using (var ctx = new PolyclinicEntities())
            {
                string query = "SELECT * FROM patient where Patient_id = @P0";
                patient = ctx.Database.SqlQuery<patient>(query, Patient_id).ToList().First();
            }
            return patient;
        }
    }
}