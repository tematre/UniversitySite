using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    public class Subject
    {
        [JsonProperty("subjectId")]
        public Guid SubjectId { get; set; }

        [JsonProperty("professorId")]
        public string ProfessorId { get; set; }

        [JsonProperty("markIds")]
        public List<Guid> MarkIds { get; set; }

        [JsonProperty("studentIds")]
        public List<Guid> StudentIds { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}