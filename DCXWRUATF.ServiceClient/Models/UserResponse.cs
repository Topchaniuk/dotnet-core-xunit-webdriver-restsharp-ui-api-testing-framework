using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace DCXWRUATF.ServiceClient.Models
{
    [DataContract]
    public class UserResponse
    {
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty("name")]
        public string Name { get; set; }


        [DataMember(Name = "job", EmitDefaultValue = false)]
        [JsonProperty("job")]
        public string Job { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty("id")]
        public string Id { get; set; }


        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
