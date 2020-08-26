using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DCXWRUATF.ServiceClient.Models
{
    [DataContract]
    public class UserRequest
    {
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty("name")]
        public string Name { get; set; }


        [DataMember(Name = "job", EmitDefaultValue = false)]
        [JsonProperty("job")]
        public string Job { get; set; }
    }
}
