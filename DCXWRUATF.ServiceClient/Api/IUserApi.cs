using DCXWRUATF.ServiceClient.Models;

namespace DCXWRUATF.ServiceClient.Api
{
    public interface IUserApi : IState
    {
        T Get<T>(int id);

        T Post<T>(UserRequest request);
    }
}
