using System.Collections.Generic;
using Domain.University;

namespace DALContracts
{
    public interface IProfessorRepository
    {
        IEnumerable<Professor> GetProfessors();
        Professor GetProfessorId(string professorId);
        void InsertProfessor(Professor professor);
        void DeleteProfessor(string professorId);
        void UpdateProfessor(Professor professor);

        void AddSubjectForProfessor(Professor professor, Subject subject);

        void SaveChanges();
    }
}