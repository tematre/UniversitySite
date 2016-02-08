using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    /// <summary>
    /// Base response. 
    /// <remarks>
    /// Mostly uses for ajax requests
    /// </remarks>
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// In all DTO objects references was raplaced to Id propperty. And here you can get any object by Id. 
        /// </summary>
        [JsonProperty("map")]
        public Dictionary<string, object> ObjectsDictionary { get; set; }

        /// <summary>
        /// Data propperty. It can be one of DTOs or <see cref="IEnumerable{T}"/> where T one of DTOs
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}