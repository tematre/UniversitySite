using System;
using System.Collections.Generic;
using Domain.University;

namespace DALContracts
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Method for get all subjects from repository
        /// </summary>
        /// <returns>List of <see cref="Subject"/> that attached to DB context</returns>
        IEnumerable<Subject> GetSubjects();
        /// <summary>
        /// Method for get <see cref="Subject"/> by Id.  
        /// </summary>
        /// <remarks>
        /// You can change it and call <see cref="SaveChanges" />
        /// </remarks>
        /// <param name="subjectId">Id of the <see cref="Subject"/></param>
        /// <returns><see cref="Subject"/> object that attached to DB context</returns>
        Subject GetSubjectId(Guid subjectId);
        /// <summary>
        /// Creates new record in DB for <paramref name="subject"/> 
        /// </summary>
        /// <remarks>You must init id for <paramref name="subject"/></remarks>
        /// <param name="subject">New subject data</param>
        void InsertSubject(Subject subject);
        /// <summary>
        /// Removes record from subjects table in DB
        /// </summary>
        /// <param name="subjectId">id of record for remove</param>
        void DeleteSubject(Guid subjectId);
        /// <summary>
        /// Updates data about subject
        /// </summary>
        /// <remarks>subject can be not attached to DB context. This method used when <see cref="SaveChanges"/> is not work</remarks>
        /// <param name="subject">subject data</param>
        void UpdateSubject(Subject subject);

        /// <summary>
        /// Add mark for student by concreate subject 
        /// </summary>
        /// <remarks>Date tame set as DateTime.Now</remarks>
        /// <param name="subject">subject data</param>
        /// <param name="student">strudent data</param>
        /// <param name="value">value of mark in [1, 5]</param>
        void AddMark(Subject subject, Student student, int value);
        /// <summary>
        /// Method that save changes to DB.
        /// </summary>
        void SaveChanges();
    }
}