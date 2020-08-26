using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace DCXWRUATF.ServiceClient.Models
{
    [DataContract]
    public class OauthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public string Error { get; set; }

        public DateTime? ExpiredDateTime { get; set; }

        public bool HasExpired => ExpiredDateTime < DateTime.Now;
    }
}
