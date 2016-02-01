using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALContracts;
using Ninject;

namespace UniversitySite.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        protected ApplicationSignInManager SignInManager { get; set; }
        [Inject]
        protected ApplicationUserManager UserManager { get; set; }
        [Inject]
        protected IStudentRepository StudentRepository { get; set; }
        [Inject]
        protected IProfessorRepository ProfessorRepository { get; set; }
        [Inject]
        protected ISubjectRepository SubjectRepository { get; set; }


        public BaseController()
        {
            StudentRepository = DependencyResolver.Current.GetService<IStudentRepository>();
            ProfessorRepository = DependencyResolver.Current.GetService<IProfessorRepository>();
            SubjectRepository = DependencyResolver.Current.GetService<ISubjectRepository>();

            UserManager = DependencyResolver.Current.GetService<ApplicationUserManager>();
            SignInManager = DependencyResolver.Current.GetService<ApplicationSignInManager>();
        }
    }
}