using DCXWRUATF.ServiceClient.Api;
using DCXWRUATF.ServiceClient.Models;

namespace DCXWRUATF.APITests.Context
{
    public class ValidationContext
    {
        public IState State { get; set; }

        public OauthToken OAuth { get; set; }

        public UserRequest UserRequest { get; set; }

        public UserResponse UserResponse { get; set; }
    }
}
