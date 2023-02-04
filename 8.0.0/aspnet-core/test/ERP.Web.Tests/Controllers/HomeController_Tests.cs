using System.Threading.Tasks;
using ERP.Models.TokenAuth;
using ERP.Web.Controllers;
using Shouldly;
using Xunit;

namespace ERP.Web.Tests.Controllers
{
    public class HomeController_Tests: ERPWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}