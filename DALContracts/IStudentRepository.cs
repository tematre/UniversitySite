using System.Collections.Generic;
using Domain.University;

namespace DALContracts
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentById(string studentId);

        void InsertStudent(Student student);
        void DeleteStudent(string studentId);
        void UpdateStudent(Student student);
        void AddSubjectForStudent(Student student, Subject subject);
        void RemoveSubjectForStudent(Student student, Subject subject);
        void SaveChanges();
    }
}