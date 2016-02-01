using System;
using System.Collections.Generic;
using Domain.University;

namespace DALContracts
{
    public interface ISubjectRepository
    {

        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectId(Guid subjectId);
        void InsertSubject(Subject subject);
        void DeleteSubject(Guid subjectId);
        void UpdateSubject(Subject subject);

        void AddMark(Subject subject, Student student, int value);

        void SaveChanges();


    }
}