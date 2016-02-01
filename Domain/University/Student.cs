using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.University
{
    public class Student
    {

        public Student()
        {
            // ReSharper disable once VirtualMemberCallInContructor
            Marks = new HashSet<Mark>();
            // ReSharper disable once VirtualMemberCallInContructor
            Subjects = new HashSet<Subject>();
        }

        [StringLength(128)]
        public string Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Surname { get; set; }



        public virtual ICollection<Mark> Marks { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}