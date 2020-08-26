using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DCXWRUATF.ServiceClient.Models
{
    [DataContract]
    public class MultipleItems
    {
        [DataMember(Name = "items", EmitDefaultValue = false)]
        [JsonProperty("items")]
        public List<object> Items { get; set; }

        [DataMember(Name = "count", EmitDefaultValue = false)]
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
