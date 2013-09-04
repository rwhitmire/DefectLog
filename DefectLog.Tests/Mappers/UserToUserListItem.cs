using AutoMapper;
using DefectLog.Models;
using DefectLog.ViewModels;
using Xunit;

namespace DefectLog.Tests.Mappers
{
    public class UserToUserListItem
    {
        public UserToUserListItem()
        {
            MappingConfig.RegisterMappings();
        }

        [Fact]
        public void Name()
        {
            var user = new User
            {
                FirstName = "Bill",
                LastName = "Jones"
            };

            var result = Mapper.Map<UserListItem>(user);

            Assert.Equal("Jones, Bill", result.Name);
        }
    }
}
