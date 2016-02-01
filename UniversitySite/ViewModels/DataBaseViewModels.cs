using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace UniversitySite.ViewModels
{
    public class ProfessorViewModel
    {
        [JsonProperty("professorId")]
        public string ProfessorId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("subjects")]
        public List<SubjectViewModel> Subjects { get; set; }
    }


    public class SubjectViewModel
    {
        [JsonProperty("subjectId")]
        public Guid SubjectId { get; set; }

        [JsonProperty("professor")]
        public ProfessorViewModel Professor { get; set; }

        [JsonProperty("marks")]
        public List<MarkViewModel> Marks { get; set; }

        [JsonProperty("Studentsstudents")]
        public List<StudentViewModel> Students { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class MarkViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("subject")]
        public SubjectViewModel Subject { get; set; }

        [JsonProperty("student")]
        public StudentViewModel Student { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }


    public class StudentViewModel
    {
        [JsonProperty("studentId")]
        public string StudentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("subjects")]
        public List<SubjectViewModel> Subjects { get; set; }

        [JsonProperty("marks")]
        public List<MarkViewModel> Marks { get; set; }

        public double AverageMark => (double) Marks.Sum(a => a.Value)/Marks.Count;
    }
}