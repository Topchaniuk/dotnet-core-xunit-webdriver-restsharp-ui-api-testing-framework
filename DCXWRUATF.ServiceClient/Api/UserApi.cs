using DCXWRUATF.ServiceClient.Client;
using DCXWRUATF.ServiceClient.Models;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace DCXWRUATF.ServiceClient.Api
{
    public class UserApi : IUserApi
    {
        private IList<RestResponseCookie> _cookies;

        private OauthToken _oauthToken;

        private ApiClient _apiClient { get; set; }

        private IRestResponse _response;

        public UserApi(string basePath)
        {
            _apiClient = new ApiClient(basePath);
        }

        public void SetOauthToken(OauthToken oauthToken)
        {
            _oauthToken = oauthToken;
        }

        public HttpStatusCode StatusCode => _response.StatusCode;

        public void SetCookies(IList<RestResponseCookie> cookies)
        {
            _cookies = cookies;
        }

        public IList<RestResponseCookie> Cookies => _response.Cookies;

        private Dictionary<string, string> queryParams = new Dictionary<string, string>();
        private Dictionary<string, string> headerParams = new Dictionary<string, string>();
        private Dictionary<string, string> formParams = new Dictionary<string, string>();
        private Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
        private Dictionary<string, string> cookies = new Dictionary<string, string>();
        private string postBody = null;
        private string[] authSettings = { };             // authentication setting, if any

        public T Get<T>(int id)
        {
            var path = $"/api/users/{id}";

            postBody = null; //Drop body if any left after previous UserApi calls.

            //MakePlateFullOfCookies(_cookies); //Pass cookies from previous request if needed.

            //headerParams["Authorization"] =  _oauthToken.TokenType + " " + _oauthToken.AccessToken; //Provide oAuth token if needed.

            //Make the HTTP request
            _response = (IRestResponse)_apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams,
                fileParams, authSettings, cookies);

            if (((int)_response.StatusCode) >= 500)
                throw new ApiException((int)_response.StatusCode, $"{_response.StatusCode}, Error calling GetUser: " + _response.Content, _response.ErrorMessage);
            if (_response.StatusCode == 0)
                throw new ApiException((int)_response.StatusCode, $"{_response.StatusCode}, Error calling GetUser: " + _response.Content, _response.ErrorMessage);

            return _apiClient.CastObjectFromResponseData<T>(_response.Content);
            //CastObjectFromResponseData is AnonymousPipeClientStream example of casting Generics to custom type, used to log if response object missed or invalid.
            //In case Generics are not used, use this to expect custom type: _apiClient.Deserialize(_response.Content, typeof(UserRequest), _response.Headers));
        }

        public T Post<T>(UserRequest request)
        {
            var path = "/api/users";

            if (request == null) throw new ApiException(400, "Required user is missed");

            //MakePlateFullOfCookies(_cookies); //Pass cookies from previous request if needed.

            //headerParams["Authorization"] =  _oauthToken.TokenType + " " + _oauthToken.AccessToken; //Provide oAuth token if needed.

            postBody = _apiClient.Serialize(request); //Http body (model) parameter

            //Make the HTTP request
            _response = (IRestResponse)_apiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams,
                fileParams, authSettings, cookies);

            if (((int)_response.StatusCode) >= 500)
                throw new ApiException((int)_response.StatusCode, $"{_response.StatusCode}, Error calling PostUser: " + _response.Content, _response.ErrorMessage);
            if (_response.StatusCode == 0)
                throw new ApiException((int)_response.StatusCode, $"{_response.StatusCode}, Error calling PostUser: " + _response.Content, _response.ErrorMessage);
            return _apiClient.CastObjectFromResponseData<T>(_response.Content);
            //Or could be nothing in response, return default(T);
        }

        private void MakePlateFullOfCookies(IList<RestResponseCookie> cook)
        {
            if (cook != null)
                foreach (var cookie in cook)
                {
                    cookies[cookie.Name] = cookie.Value;
                }
        }
    }
}
