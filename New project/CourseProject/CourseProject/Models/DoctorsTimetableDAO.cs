using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class FIO
    {
        public string surname;
        public string name;
        public string patronymic;
        public FIO(string surname,string name,string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    
    public class DoctorsTimetableDAO
    {
        public List<docrors_timetable> GetTimetable()
        {
            List<docrors_timetable> timetable = new List<docrors_timetable>();
            try
            {
                using (var ctx = new PolyclinicEntities3())
                {
                    string query = "SELECT * FROM docrors_timetable";
                    timetable.AddRange(ctx.Database.SqlQuery<docrors_timetable>(query).ToList());
                   
                }
            }
            catch (Exception ex)
            {}
            return timetable;
        }

        public void CreateTimetable(docrors_timetable model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities3())
                {
                    string query = "INSERT INTO docrors_timetable (id_doctor, Monday, Tuesday, Wednesday, Thursday, Friday) VALUES(@P0, @P1, @P2, @P3, @P4, @P5)";
                    List<object> parameterList = new List<object>{

                        model.id_doctor,
                        model.Monday,
                        model.Tuesday,
                        model.Wednesday,
                        model.Thursday,
                        model.Friday
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                }
            }
            catch (Exception ex)
            { }
        }

        public void EditTimetable(docrors_timetable model, int Timetable_id)
        {
            using (var ctx = new PolyclinicEntities3())
            {
                string query = "update docrors_timetable set id_doctor=@P1, Monday=@P2, Tuesday=@P3, Wednesday=@P4, Thursday=@P5, Friday=@P6 where Timetable_id=@P0";
                List<object> parameterList = new List<object>{
                        model.Timetable_id,
                        model.id_doctor,
                        model.Monday,
                        model.Tuesday,
                        model.Wednesday,
                        model.Thursday,
                        model.Friday
                    };
                object[] parameters = parameterList.ToArray();
                int result = ctx.Database.ExecuteSqlCommand(query, parameters);
            }
        }
    }
}