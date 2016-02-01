using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Identity;
using DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UniversitySite.Helper;
using UniversitySite.ViewModels;
using Professor = Domain.University.Professor;
using Student = Domain.University.Student;
using Subject = Domain.University.Subject;

namespace UniversitySite.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }


        [JsonNetFilter]
        [AllowAnonymous]
        public ActionResult GetAllProfessors()
        {
            return Json(ProfessorRepository.GetProfessors().ToResponse(), JsonRequestBehavior.AllowGet);
        }

        [JsonNetFilter]
        [AllowAnonymous]
        public ActionResult GetAllStudents()
        {
            return Json(StudentRepository.GetStudents().ToResponse(), JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterStudent(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser {UserName = model.Login};
                    var result = await UserManager.CreateAsync(user, model.Password);
                    await UserManager.AddToRoleAsync(user.Id, Constants.Roles.Student);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false, false);

                        var studentInfo = new Student
                        {
                            Id = user.Id,
                            Name = model.StudentRegisterVewModel.Name,
                            Surname = model.StudentRegisterVewModel.Surname
                        };
                        StudentRepository.InsertStudent(studentInfo);
                        if (model.StudentRegisterVewModel.SubjectIds != null)
                        {
                            foreach (var subjectId in model.StudentRegisterVewModel.SubjectIds)
                            {
                                var subject = SubjectRepository.GetSubjectId(Guid.Parse(subjectId));

                                StudentRepository.AddSubjectForStudent(studentInfo, subject);
                            }
                        }


                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
            }
            catch (Exception e)
            {
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterProfessor(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Login, Email = model.Login};
                var result = await UserManager.CreateAsync(user, model.Password);
                await UserManager.AddToRoleAsync(user.Id, Constants.Roles.Professor);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);

                    var professorInfo = new Professor
                    {
                        Id = user.Id,
                        Name = model.ProfessorRegisterVewModel.Name,
                        Surname = model.ProfessorRegisterVewModel.Surname
                    };

                    ProfessorRepository.InsertProfessor(professorInfo);
                    if (model.ProfessorRegisterVewModel.Subjects != null)
                    {
                        foreach (var subjectName in model.ProfessorRegisterVewModel.Subjects)
                        {
                            var subject = new Subject
                            {
                                Id = Guid.NewGuid(),
                                Name = subjectName,
                                Professor = professorInfo
                            };

                            SubjectRepository.InsertSubject(subject);
                            ProfessorRepository.AddSubjectForProfessor(professorInfo, subject);
                        }
                    }


                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var model = new EditViewModel();
            if (User.IsInRole(Constants.Roles.Professor))
            {
                model.Professor = ProfessorRepository.GetProfessorId(userId).ToViewModel();
            }
            if (User.IsInRole(Constants.Roles.Student))
            {
                model.Student = StudentRepository.GetStudentById(userId).ToViewModel();
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return RedirectToAction("Index", "Admin");
        }

        [JsonNetFilter]
        [AllowAnonymous]
        public JsonResult GetProfessorViewModel(string id)
        {
            var professor = ProfessorRepository.GetProfessorId(id ?? User.Identity.GetUserId());

            return Json(professor.ToResponse(), JsonRequestBehavior.AllowGet);
        }

        [JsonNetFilter]
        [AllowAnonymous]
        public JsonResult GetStudentViewModel(string id)
        {
            var professor = StudentRepository.GetStudentById(id ?? User.Identity.GetUserId());

            return Json(professor.ToResponse(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditProfessor(ProfessorViewModel vm)
        {
            var student = ProfessorRepository.GetProfessorId(vm.ProfessorId);
            student.Name = vm.Name;
            student.Surname = vm.Surname;
            ProfessorRepository.SaveChanges();

            return PartialView("_editProfessor", vm);
        }


        public ActionResult AddMarkForStudent(ProfessorViewModel vm)
        {
            return PartialView("_editProfessor", vm);
        }


        public ActionResult EditStudent(StudentViewModel vm)
        {
            var student = StudentRepository.GetStudentById(vm.StudentId);
            student.Name = vm.Name;
            student.Surname = vm.Surname;

            return PartialView("_editStudent", vm);
        }


        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(),
                    vm.OldPassword,
                    vm.NewPassword);
                if (result.Succeeded)
                {
                    return PartialView("_changePassword", vm);
                }

                AddErrors(result);
            }
            return PartialView("_changePassword", vm);
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UserManager != null)
                {
                    UserManager.Dispose();
                    UserManager = null;
                }

                if (SignInManager != null)
                {
                    SignInManager.Dispose();
                    SignInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}