using System.Collections.Generic;
using Domain.University;

namespace DALContracts
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Method for get all students from repository
        /// </summary>
        /// <returns>List of <see cref="Student"/> that attached to DB context</returns>
        IEnumerable<Student> GetStudents();

        /// <summary>
        /// Method for get <see cref="Student"/> by Id.  
        /// </summary>
        /// <remarks>
        /// You can change it and call <see cref="SaveChanges" />
        /// </remarks>
        /// <param name="studentId">Id of the <see cref="Student"/></param>
        /// <returns><see cref="Student"/> object that attached to DB context (in <seealso cref="_GUID"/> format)</returns>
        Student GetStudentById(string studentId);

        /// <summary>
        /// Creates new record in DB for <paramref name="student"/> 
        /// </summary>
        /// <remarks>You must init id for <paramref name="student"/></remarks>
        /// <param name="student">New student data</param>
        void InsertStudent(Student student);
        /// <summary>
        /// Removes record from students table in DB
        /// </summary>
        /// <param name="studentId">id of record for remove (in <seealso cref="_GUID"/> format)</param>
        void DeleteStudent(string studentId);

        /// <summary>
        /// Updates data about student
        /// </summary>
        /// <remarks>student can be not attached to DB context. This method used when <see cref="SaveChanges"/> is not work</remarks>
        /// <param name="student">student data</param>
        void UpdateStudent(Student student);

        /// <summary>
        /// Updates data about student (add new subject)
        /// </summary>
        /// <param name="student">student <remarks>can be not attached to DB</remarks></param>
        /// <param name="subject">subject <remarks>can be not attached to DB</remarks></param>
        void AddSubjectForStudent(Student student, Subject subject);

        /// <summary>
        /// Updates data about student (remove info about subject from students schedule)
        /// </summary>
        /// <param name="student">student <remarks>can be not attached to DB</remarks></param>
        /// <param name="subject">subject <remarks>can be not attached to DB</remarks></param>
        void RemoveSubjectForStudent(Student student, Subject subject);
        /// <summary>
        /// Method that save changes to DB.
        /// </summary>
        void SaveChanges();
    }
}