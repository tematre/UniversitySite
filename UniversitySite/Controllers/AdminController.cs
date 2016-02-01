using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Domain.University;
using UniversitySite.ViewModels;

namespace UniversitySite.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            var vm = new AdminReportViewModel();

            var studentsCount = StudentRepository.GetStudents().Count();

            vm.SubjectsThatInludesAllStudents =
                SubjectRepository.GetSubjects()
                    .Where(a => a.Students.Count == studentsCount)
                    .Select(a => new SubjectViewModel
                    {
                        Professor = new ProfessorViewModel {Name = a.Professor.Name, Surname = a.Professor.Surname},
                        Name = a.Name
                    }).ToList();

            var allMarks = SubjectRepository.GetSubjects().SelectMany(a => a.Marks).ToArray();
            var averageMark = (double) allMarks.Sum(a => a.Value)/allMarks.Length;

            vm.StudentsWithGoodAverageMark =
                StudentRepository.GetStudents().Select(a => new StudentViewModel
                {
                    Marks = a.Marks.Select(b => new MarkViewModel {Value = b.Value}).ToList(),
                    Name = a.Name
                }).Where(a => a.AverageMark >= averageMark).ToList();

            var subjects = SubjectRepository.GetSubjects().ToArray();
            if (subjects.Length > 0)
            {
                var minStudentsCount = subjects.Min(a => a.Students.Count);

                vm.ProfessorsWithMinStudentsCount =
                    ProfessorRepository.GetProfessors()
                        .Where(a => a.Subjects.Any(b => b.Students.Count == minStudentsCount))
                        .Select(a => new ProfessorViewModel {Name = a.Name, Surname = a.Surname}).ToList();
            }
            else
            {
                vm.ProfessorsWithMinStudentsCount = new List<ProfessorViewModel>();
            }


            return View(vm);
        }

        [AllowAnonymous]
        public ActionResult AddSubjectToProfessor(string professorId, string subjectName)
        {
            var professor = ProfessorRepository.GetProfessorId(professorId);

            if (professor == null)
            {
                throw new InvalidOperationException($"Can\'t find professor with id. \"{professorId}\"");
            }

            var subject = new Subject {Id = Guid.NewGuid(), Name = subjectName};

            SubjectRepository.InsertSubject(subject);
            ProfessorRepository.AddSubjectForProfessor(professor, subject);


            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public ActionResult RemoveSubject(string id)
        {
            var subject = SubjectRepository.GetSubjectId(Guid.Parse(id));

            if (subject == null)
            {
                throw new InvalidOperationException($"Can\'t find subject with id. \"{id}\"");
            }
            SubjectRepository.DeleteSubject(Guid.Parse(id));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public ActionResult RenameSubject(string id, string name)
        {
            var subject = SubjectRepository.GetSubjectId(Guid.Parse(id));

            if (subject == null)
            {
                throw new InvalidOperationException($"Can\'t find subject with id. \"{id}\"");
            }

            subject.Name = name;
            SubjectRepository.UpdateSubject(subject);


            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}