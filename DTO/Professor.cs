using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    public class Professor
    {
        [JsonProperty("professorId")]
        public string ProfessorId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("subjectIds")]
        public List<Guid> SubjectIds { get; set; }


        [JsonProperty("studentsCount")]
        public int StudentsCount { get; set; }
    }
}