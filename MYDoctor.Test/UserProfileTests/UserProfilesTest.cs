using MYDoctor.Infrastructure.Repository;
using MYDoctor.Test.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace MYDoctor.Test.UserProfileTests
{
    public class UserProfilesTest:TestBase
    {
        
        
        [Fact]
        public void GetUserProfiles()
        {
            // arrange
            var userProfileRepository = new UserProfileRepository(Context);
            // act
            var allUsers = userProfileRepository.GetAll();
            //assert
            Assert.Equal(2,allUsers.Count());
        }
        [Fact]
        public async Task GetUserProfileByEmail()
        {
            // arrange
            var userProfileRepository = new UserProfileRepository(Context);
            // act
            var user =await userProfileRepository.GetUserProfileAsync("Test@gmail.com");
            //assert
            Assert.Equal("Test", user.Model.Name);
        }
    }
}
