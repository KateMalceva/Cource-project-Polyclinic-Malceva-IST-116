﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyclinicCourseProject.Models
{
    public class DoctorDAO
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<doctor> GetDoctor()
        {
            List<doctor> doctors = new List<doctor>();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM doctor";
                    doctors.AddRange(ctx.Database.SqlQuery<doctor>(query).ToList());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return doctors;
        }

        public void CreateDoctor(doctor model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "INSERT INTO doctor (Surname, Name, Patronymic, Date_of_birth, Phone_number, id_specialty, Education) VALUES(@P0, @P1, @P2, @P3, @P4, @P5, @P6)";
                    List<object> parameterList = new List<object>{
                        model.Surname,
                        model.Name,
                        model.Patronymic,
                        model.Date_of_birth,
                        model.Phone_number,
                        model.id_specialty,
                        model.Education
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные успешно добавлены в doctor");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }

        public void EditDoctor(doctor model, int Doctor_id)
        {
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "update doctor set Surname=@P1, Name=@P2, Patronymic=@P3, Date_of_birth=@P4, Phone_number=@P5, id_specialty=@P6, Education=@P7 where Doctor_id=@P0";
                    List<object> parameterList = new List<object>{
                        model.Doctor_id,
                        model.Surname,
                        model.Name,
                        model.Patronymic,
                        model.Date_of_birth,
                        model.Phone_number,
                        model.id_specialty,
                        model.Education
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные в doctor успешно изменены");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }


        public doctor DetailsDoctor(int Doctor_id)
        {
            doctor doctor = new doctor();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM doctor where Doctor_id = @P0";
                    doctor = ctx.Database.SqlQuery<doctor>(query, Doctor_id).ToList().First();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return doctor;
        }
    }
}
