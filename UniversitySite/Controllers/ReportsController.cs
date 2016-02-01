using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using UniversitySite.Helper;

namespace UniversitySite.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllStudents()
        {
            return View();
        }
        public ActionResult AllProfessors()
        {
            return View();
        }

        [JsonNetFilter]
        public ActionResult GetAllStudents()
        {
            return Json(StudentRepository.GetStudents().ToResponse(), JsonRequestBehavior.AllowGet);
        }
        [JsonNetFilter]
        public ActionResult GetAllProfessors()
        {
            return Json(ProfessorRepository.GetProfessors().ToResponse(), JsonRequestBehavior.AllowGet);
        }
    }
}