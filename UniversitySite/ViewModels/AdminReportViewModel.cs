using System.Collections.Generic;

namespace UniversitySite.ViewModels
{
    


    public class AdminReportViewModel
    {
         public List<SubjectViewModel> SubjectsThatInludesAllStudents { get; set; } 
         public List<StudentViewModel> StudentsWithGoodAverageMark { get; set; }
         public List<ProfessorViewModel> ProfessorsWithMinStudentsCount { get; set; }

    }
}