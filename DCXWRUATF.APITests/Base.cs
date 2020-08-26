using DCXWRUATF.APITests.Context;
using DCXWRUATF.ServiceClient.Api;
using DCXWRUATF.ServiceClient.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DCXWRUATF.APITests
{
    public class Base
    {
        protected ValidationContext _validationContext;

        public IConfiguration Configuration => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public Base()
        {
            _validationContext = new ValidationContext();
        }

        public void SetService(IState newState)
        {
            if (_validationContext.State != null)
                newState.SetCookies(_validationContext.State.Cookies);

            if (_validationContext.OAuth.HasExpired)
            {
                //_validationContext.OAuth = GenerateOauth2Token();
            }

            newState.SetOauthToken(_validationContext.OAuth);

            _validationContext.State = newState;
        }

        public T CastObjectFromResponseData<T>(object obj)
        {
            if(obj == null)
                throw new ApiException("Response data not received or response has different model");
            var json = JsonConvert.SerializeObject(obj);
            var res = JsonConvert.DeserializeObject<T>(json);
            return res;
        }
    }
}
