using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class ListSpecialtyDAO
    {
        public List<list_of_specialty> GetSpecialty()
        {
            List<list_of_specialty> specialties = new List<list_of_specialty>();
            try
            {
                using (var ctx = new PolyclinicEntities3())
                {
                    string query = "SELECT * FROM list_of_specialty";
                    specialties.AddRange(ctx.Database.SqlQuery<list_of_specialty>(query).ToList());
                }
            }
            catch (Exception ex)
            { }
            return specialties;
        }
        
        public void CreateSpecialty(list_of_specialty model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities3())
                {
                    string query = "INSERT INTO list_of_specialty (Spetialty_name, Description) VALUES(@P0, @P1)";
                    List<object> parameterList = new List<object>{
                        
                        model.Spetialty_name,
                        model.Description
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);

                }
            }
            catch (Exception ex)
            { }
        }

        public void EditSpecialty(list_of_specialty model, int Specialty_id)
        {
            using (var ctx = new PolyclinicEntities3())
            {
                string query = "update list_of_specialty set Spetialty_name=@P1, Description=@P2 where Specialty_id=@P0";
                List<object> parameterList = new List<object>{
                        Specialty_id,
                        model.Spetialty_name,
                        model.Description
                    };
                object[] parameters = parameterList.ToArray();
                int result = ctx.Database.ExecuteSqlCommand(query, parameters);
            }
        }

        public list_of_specialty DetailsSpecialty(int Specialty_id)
        {
            List<list_of_specialty> listSpecialty = new List<list_of_specialty>();
            list_of_specialty list = new list_of_specialty();
            using (var ctx = new PolyclinicEntities3())
            {
                string query = "SELECT * FROM list_of_specialty where Specialty_id = @P0";
                list = ctx.Database.SqlQuery<list_of_specialty>(query, Specialty_id).ToList().First();
            }
            return list;
        }
    }
}