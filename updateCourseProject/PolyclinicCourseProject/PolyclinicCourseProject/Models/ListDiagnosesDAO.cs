﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyclinicCourseProject.Models
{
    public class ListDiagnosesDAO
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<list_of_diagnoses> GetDiagnoses()
        {
            List<list_of_diagnoses> diagnoses = new List<list_of_diagnoses>();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM list_of_diagnoses";
                    diagnoses.AddRange(ctx.Database.SqlQuery<list_of_diagnoses>(query).ToList());
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return diagnoses;
        }

        public void CreateDiagnosis(list_of_diagnoses model)
        {
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "INSERT INTO list_of_diagnoses (Diagnose_name, Description) VALUES(@P0, @P1)";
                    List<object> parameterList = new List<object>{

                        model.Diagnose_name,
                        model.Description
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные успешно добавлены в list_of_diagnoses");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }

        public void EditDiagnosis(list_of_diagnoses model, int List_diagnoses_id)
        {
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "update list_of_diagnoses set Diagnose_name=@P1, Description=@P2 where List_diagnoses_id=@P0";
                    List<object> parameterList = new List<object>{
                        List_diagnoses_id,
                        model.Diagnose_name,
                        model.Description
                    };
                    object[] parameters = parameterList.ToArray();
                    int result = ctx.Database.ExecuteSqlCommand(query, parameters);
                    logger.Info("Данные в list_of_diagnoses успешно изменены");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
        }

        public list_of_diagnoses DetailsDiagnosis(int List_diagnoses_id)
        {
            list_of_diagnoses list = new list_of_diagnoses();
            try
            {
                using (var ctx = new PolyclinicEntities1())
                {
                    string query = "SELECT * FROM list_of_diagnoses where List_diagnoses_id = @P0";
                    list = ctx.Database.SqlQuery<list_of_diagnoses>(query, List_diagnoses_id).ToList().First();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: ", ex);
            }
            return list;
        }
    }
}