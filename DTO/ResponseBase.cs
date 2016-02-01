using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    public class ResponseBase
    {
        [JsonProperty("map")]
        public Dictionary<string, object> ObjectsDictionary { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}