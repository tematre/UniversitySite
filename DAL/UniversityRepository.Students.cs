using System.Collections.Generic;
using System.Linq;
using DALContracts;
using Domain.University;

namespace DAL
{
    public partial class UniversityRepository : IStudentRepository
    {
        public IEnumerable<Student> GetStudents()
        {
            return _db.Students.ToArray();
        }

        public Student GetStudentById(string studentId)
        {
            return _db.Students.FirstOrDefault(a => a.Id == studentId);
        }

        public void InsertStudent(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
        }

        public void DeleteStudent(string studentId)
        {
            var student = GetStudentById(studentId);
            if (student != null)
            {
                _db.Students.Remove(student);
            }
            _db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            _db.Students.Attach(student);
            _db.SaveChanges();
        }

        public void AddSubjectForStudent(Student student, Subject subject)
        {
            _db.Students.Attach(student);
            _db.Subjects.Attach(subject);


            student.Subjects.Add(subject);

            _db.SaveChanges();
        }

        public void RemoveSubjectForStudent(Student student, Subject subject)
        {
            _db.Students.Attach(student);
            _db.Subjects.Attach(subject);


            student.Subjects.Remove(subject);

            _db.SaveChanges();
        }
    }
}