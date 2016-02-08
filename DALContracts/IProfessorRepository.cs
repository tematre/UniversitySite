using System.Collections.Generic;
using Domain.University;

namespace DALContracts
{
    public interface IProfessorRepository
    {
        /// <summary>
        /// Method for get all professors from repository
        /// </summary>
        /// <returns>List of <see cref="Professor"/> that attached to DB context</returns>
        IEnumerable<Professor> GetProfessors();


        /// <summary>
        /// Method for get <see cref="Professor"/> by Id.  You can change it and call <see cref="SaveChanges"/>
        /// </summary>
        /// <param name="professorId">Id of the <see cref="Professor"/></param>
        /// <returns><see cref="Professor"/> object that attached to DB context (in <seealso cref="_GUID"/> format)</returns>
        Professor GetProfessorById(string professorId);

        /// <summary>
        /// Creates new record in DB for <paramref name="professor"/> 
        /// </summary>
        /// <remarks>You must init id for <paramref name="professor"/></remarks>
        /// <param name="professor">New professor data</param>
        void InsertProfessor(Professor professor);
        /// <summary>
        /// Removes record from professors table in DB
        /// </summary>
        /// <param name="professorId">id of record for remove (in <seealso cref="_GUID"/> format)</param>
        void DeleteProfessor(string professorId);

        /// <summary>
        /// Updates data about professor
        /// </summary>
        /// <remarks>Professor can be not attached to DB context. This method used when <see cref="SaveChanges"/> is tot work</remarks>
        /// <param name="professor"></param>
        void UpdateProfessor(Professor professor);

        /// <summary>
        /// Updates data about professor (add new subject)
        /// </summary>
        /// <param name="professor">professor <remarks>can be not attached to DB</remarks></param>
        /// <param name="subject">subject <remarks>can be not attached to DB</remarks></param>
        void AddSubjectForProfessor(Professor professor, Subject subject);

        /// <summary>
        /// Method that save changes to DB.
        /// </summary>
        void SaveChanges();
    }
}