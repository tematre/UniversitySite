using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.University
{
    /// <summary>
    /// Class that maps to Subjects table
    /// </summary>
    public class Subject
    {
        public Subject()
        {
            // ReSharper disable once VirtualMemberCallInContructor
            Marks = new HashSet<Mark>();
            // ReSharper disable once VirtualMemberCallInContructor
            Students = new HashSet<Student>();
        }

        public Guid Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}