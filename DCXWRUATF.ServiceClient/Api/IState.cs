using DCXWRUATF.ServiceClient.Models;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace DCXWRUATF.ServiceClient.Api
{
    public interface IState
    {
        void SetOauthToken(OauthToken oauthToken);

        HttpStatusCode StatusCode { get; }

        void SetCookies(IList<RestResponseCookie> cookies);

        IList<RestResponseCookie> Cookies { get; }
    }
}
