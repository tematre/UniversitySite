using System;
using Newtonsoft.Json;

namespace DTO
{
    public class Mark
    {

        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("subject")]
        public Guid Subject { get; set; }

        [JsonProperty("student")]
        public string Student { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}