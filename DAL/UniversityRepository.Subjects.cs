using System;
using System.Collections.Generic;
using System.Linq;
using DALContracts;
using Domain.University;

namespace DAL
{
    public partial class UniversityRepository : ISubjectRepository
    {
        public IEnumerable<Subject> GetSubjects()
        {
            return _db.Subjects.ToArray();
        }

        public Subject GetSubjectId(Guid subjectId)
        {
            return _db.Subjects.FirstOrDefault(a => a.Id == subjectId);
        }

        public void InsertSubject(Subject subject)
        {
            _db.Subjects.Add(subject);
            _db.SaveChanges();
        }

        public void DeleteSubject(Guid subjectId)
        {
            var subject = GetSubjectId(subjectId);
            if (subject != null)
            {
                foreach (var mark in subject.Marks.ToArray())
                {
                    _db.Marks.Remove(mark);
                }
                foreach (var student in subject.Students)
                {
                    student.Subjects.Remove(subject);
                }
                subject.Professor.Subjects.Remove(subject);
                _db.Subjects.Remove(subject);
            }
            _db.SaveChanges();
        }

        public void UpdateSubject(Subject subject)
        {
            _db.Subjects.Attach(subject);

            _db.SaveChanges();
        }

        public void AddMark(Subject subject, Student student, int value)
        {
            var mark = new Mark
            {
                Id = Guid.NewGuid(),
                Student = student,
                Date = DateTime.Now,
                StudentId = student.Id,
                Value = value,
                SubjectId = subject.Id,
                Subject = subject
            };

            _db.Subjects.Attach(subject);
            _db.Students.Attach(student);
            _db.Marks.Add(mark);
            SaveChanges();
        }
    }
}