using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class ListSpecialtyController : Controller
    {
        private readonly ListSpecialtyDAO specialtyDAO = new ListSpecialtyDAO();

        // GET
        public ActionResult Index()
        {
            var specialty = specialtyDAO.GetSpecialty();
            return View(specialty);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(list_of_specialty model)
        {
            try
            {
                specialtyDAO.CreateSpecialty(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var specialty = specialtyDAO.GetSpecialty().FirstOrDefault(x => x.Specialty_id == id);
            return View(specialty);
        }

        [HttpPost]
        public ActionResult Edit(list_of_specialty model)
        {
            try
            {
                int id = Convert.ToInt32(ControllerContext.RouteData.Values.Values.ElementAt(2));
                specialtyDAO.EditSpecialty(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }

        //Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var specialty = specialtyDAO.DetailsSpecialty(id);
            return View(specialty);
        }
    }
}