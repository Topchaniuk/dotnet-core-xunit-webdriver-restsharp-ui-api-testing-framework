using DCXWRUATF.ServiceClient.Api;
using DCXWRUATF.ServiceClient.Models;
using FluentAssertions;
using Xunit;

namespace DCXWRUATF.APITests
{
    public class UsersTests : Base
    {
        public UsersTests() : base() { }

        [Fact]
        public void Create_new_user()
        {
            //Arrange
            IUserApi userService = new UserApi(Configuration["BaseUrl"]);
            _validationContext.UserRequest = new UserRequest
            {
                Name = "Joe",
                Job = "Dancer"
            };

            //Act
            _validationContext.UserResponse = userService.Post<UserResponse>(_validationContext.UserRequest);

            //Assert
            _validationContext.UserResponse.Should().BeEquivalentTo(_validationContext.UserRequest, filter => filter.ExcludingMissingMembers());
            var createdUser = userService.Get<UserResponse>(int.Parse(_validationContext.UserResponse.Id));
            createdUser.Should().BeEquivalentTo(_validationContext.UserResponse);
        }
    }
}
