using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.University
{
    /// <summary>
    /// Class that maps to table Marks. You can use it for generate reports.
    /// </summary>
    public class Mark
    {
        public Guid Id { get; set; }

        public string StudentId { get; set; }
        public Guid SubjectId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Column("Mark")]
        public int Value { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}