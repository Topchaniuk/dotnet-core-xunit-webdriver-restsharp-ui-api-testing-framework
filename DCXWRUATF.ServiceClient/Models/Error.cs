using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DCXWRUATF.ServiceClient.Models
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
