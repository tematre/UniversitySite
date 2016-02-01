using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    public class Student
    {

        [JsonProperty("studentId")]
        public string StudentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("subjectIds")]
        public List<Guid> SubjectIds { get; set; }

        [JsonProperty("markIds")]
        public List<Guid> MarkIds { get; set; }

        [JsonProperty("comonAverageMark")]
        public double ComonAverageMark { get; set; }

        [JsonProperty("averageMarks")]
        public Dictionary<string, double> AverageMarks { get; set; }
    }
}