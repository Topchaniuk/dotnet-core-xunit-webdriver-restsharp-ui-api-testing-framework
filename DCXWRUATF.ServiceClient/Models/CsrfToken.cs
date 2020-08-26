using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DCXWRUATF.ServiceClient.Models
{
    [DataContract]
    public class CsrfToken
    {
        [DataMember(Name = "token", EmitDefaultValue = false)]
        [JsonProperty("token")]
        public string Token { get; set; }

        [DataMember(Name = "headerName", EmitDefaultValue = false)]
        [JsonProperty("headerName")]
        public string HeaderName { get; set; }
    }
}
