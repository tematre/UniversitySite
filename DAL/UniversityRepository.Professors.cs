using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using DALContracts;
using Domain.University;

namespace DAL
{
    public partial class UniversityRepository : IProfessorRepository
    {


        public IEnumerable<Professor> GetProfessors()
        {
            return _db.Professors.ToArray();
        }

        public Professor GetProfessorId(string professorId)
        {
            return _db.Professors.FirstOrDefault(a => a.Id == professorId);
        }

        public void InsertProfessor(Professor professor)
        {
            _db.Professors.Add(professor);
            _db.SaveChanges();
        }

        public void DeleteProfessor(string professorId)
        {
            var professor = GetProfessorId(professorId);
            if (professor != null)
            {
                _db.Professors.Remove(professor);
            }
            _db.SaveChanges();
        }

        public void UpdateProfessor(Professor professor)
        {
            _db.Professors.Attach(professor);
            _db.SaveChanges();
        }

        public void AddSubjectForProfessor(Professor professor, Subject subject)
        {
            _db.Professors.Attach(professor);
            _db.Subjects.Attach(subject);


            professor.Subjects.Add(subject);

            _db.SaveChanges();
        }
    }
}