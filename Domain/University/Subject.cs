using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.University
{
    public class Subject
    {

        public Subject()
        {
            // ReSharper disable once VirtualMemberCallInContructor
            Marks = new HashSet<Mark>();
            // ReSharper disable once VirtualMemberCallInContructor
            Students = new HashSet<Student>();
        }

        public System.Guid Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}