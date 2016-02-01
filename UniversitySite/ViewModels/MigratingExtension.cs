using System;
using System.Collections.Generic;
using System.Linq;
using Domain.University;

namespace UniversitySite.ViewModels
{
    public static class MigratingExtension
    {
        private static void InitData(
            Dictionary<string, ProfessorViewModel> professorVmDic,
            Dictionary<string, StudentViewModel> studentVmDic,
            Dictionary<Guid, SubjectViewModel> subjectVmDic,
            Dictionary<Guid, MarkViewModel> marksVmDic,
            Dictionary<string, Professor> professorDic,
            Dictionary<string, Student> studentDic,
            Dictionary<Guid, Subject> subjectDic,
            Dictionary<Guid, Mark> marksDic
            )
        {
            foreach (var professorVm in professorVmDic)
            {
                professorVm.Value.ProfessorId = professorVm.Key;
                professorVm.Value.Name = professorDic[professorVm.Key].Name;
                professorVm.Value.Surname = professorDic[professorVm.Key].Surname;
                professorVm.Value.Subjects =
                    professorDic[professorVm.Key].Subjects.Select(a => subjectVmDic[a.Id]).ToList();
            }

            foreach (var studentVm in studentVmDic)
            {
                studentVm.Value.StudentId = studentVm.Key;
                studentVm.Value.Name = studentDic[studentVm.Key].Name;
                studentVm.Value.Surname = studentDic[studentVm.Key].Surname;
                studentVm.Value.Subjects = studentDic[studentVm.Key].Subjects.Select(a => subjectVmDic[a.Id]).ToList();
                studentVm.Value.Marks = studentDic[studentVm.Key].Marks.Select(a => marksVmDic[a.Id]).ToList();
            }

            foreach (var subjectViewModel in subjectVmDic)
            {
                subjectViewModel.Value.SubjectId = subjectViewModel.Key;
                subjectViewModel.Value.Name = subjectDic[subjectViewModel.Key].Name;
                subjectViewModel.Value.Students =
                    subjectDic[subjectViewModel.Key].Students.Select(a => studentVmDic[a.Id]).ToList();
                subjectViewModel.Value.Professor = professorVmDic[subjectDic[subjectViewModel.Key].Professor.Id];
                subjectViewModel.Value.Marks =
                    subjectDic[subjectViewModel.Key].Marks.Select(
                        a => marksVmDic.ContainsKey(a.Id) ? marksVmDic[a.Id] : null).ToList();
            }

            foreach (var markVm in marksVmDic)
            {
                markVm.Value.Id = markVm.Key;
                markVm.Value.Student = studentVmDic[marksDic[markVm.Key].StudentId];

                markVm.Value.Subject = subjectVmDic[marksDic[markVm.Key].SubjectId];
                markVm.Value.Value = marksDic[markVm.Key].Value;
            }
        }

        public static ProfessorViewModel ToViewModel(this Professor that)
        {
            var result = new ProfessorViewModel();

            var studentVmDic = that.Subjects.SelectMany(a => a.Students)
                .GroupBy(a => a.Id)
                .ToDictionary(a => a.Key, a => new StudentViewModel());
            var subjecVmtDic = that.Subjects.ToDictionary(a => a.Id, a => new SubjectViewModel());
            var marksVmDic =
                that.Subjects.SelectMany(a => a.Students)
                    .SelectMany(a => a.Marks)
                    .GroupBy(a => a.Id)
                    .ToDictionary(a => a.Key, a => new MarkViewModel());

            var studentDic = that.Subjects.SelectMany(a => a.Students)
                .GroupBy(a => a.Id)
                .ToDictionary(a => a.Key, a => a.FirstOrDefault());
            var subjectDic = that.Subjects.ToDictionary(a => a.Id, a => a);
            var marksDic =
                that.Subjects.SelectMany(a => a.Students)
                    .SelectMany(a => a.Marks)
                    .GroupBy(a => a.Id)
                    .ToDictionary(a => a.Key, a => a.FirstOrDefault());

            var professorVmDic = new Dictionary<string, ProfessorViewModel>
            {
                [that.Id] = result
            };
            var professorDic = new Dictionary<string, Professor>
            {
                [that.Id] = that
            };

            InitData(professorVmDic, studentVmDic, subjecVmtDic, marksVmDic,
                professorDic, studentDic, subjectDic, marksDic);

            return result;
        }

        public static StudentViewModel ToViewModel(this Student that)
        {
            var result = new StudentViewModel();

            var marksDic = that.Marks.ToDictionary(a => a.Id, a => a);
            var markVmDic = that.Marks.ToDictionary(a => a.Id, a => new MarkViewModel());

            var subjectDic = that.Subjects.ToDictionary(a => a.Id, a => a);
            var subjecVmtDic = that.Subjects.ToDictionary(a => a.Id, a => new SubjectViewModel());

            var professorDic = that.Subjects.Select(a => a.Professor)
                .GroupBy(a => a.Id)
                .ToDictionary(a => a.Key, a => a.FirstOrDefault());
            var professorVmDic = that.Subjects.Select(a => a.Professor)
                .GroupBy(a => a.Id)
                .ToDictionary(a => a.Key, a => new ProfessorViewModel());

            var studentVmDic = new Dictionary<string, StudentViewModel>
            {
                [that.Id] = result
            };
            var studentDic = new Dictionary<string, Student>
            {
                [that.Id] = that
            };


            InitData(professorVmDic, studentVmDic, subjecVmtDic, markVmDic,
                professorDic, studentDic, subjectDic, marksDic);

            return result;
        }
    }
}