using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.University
{
    /// <summary>
    /// Class that maps to Professors table
    /// </summary>
    public class Professor
    {
        public Professor()
        {
            // ReSharper disable once VirtualMemberCallInContructor
            Subjects = new HashSet<Subject>();
        }

        [StringLength(128)]
        public string Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Surname { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}