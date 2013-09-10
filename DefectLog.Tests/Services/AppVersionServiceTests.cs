using System.Collections.Generic;
using System.Linq;
using DefectLog.Core.Domain;
using DefectLog.Core.Models;
using DefectLog.Core.Services;
using Moq;
using Xunit;

namespace DefectLog.Tests.Services
{
    public class AppVersionServiceTests
    {
        private readonly Mock<IRepository<AppVersion>> _repository;
        private readonly AppVersionService _appVersionService;

        public AppVersionServiceTests()
        {
            _repository = new Mock<IRepository<AppVersion>>();
            _appVersionService = new AppVersionService(_repository.Object);
        }

        [Fact]
        public void VersionExistsShouldAllowEditOfExisingRecord()
        {
            var appVersions = new List<AppVersion>
            {
                new AppVersion {Id = 1, VersionNumber = "1.0"}
            };

            _repository.Setup(x => x.GetAll()).Returns(appVersions.AsQueryable());

            var result = _appVersionService.VersionExists(1, "1.0");
            Assert.False(result);
        }

        [Fact]
        public void VersionExistsShouldReturnTrueWhenVersionExists()
        {
            var appVersions = new List<AppVersion>
            {
                new AppVersion {Id = 1, VersionNumber = "1.0"}
            };

            _repository.Setup(x => x.GetAll()).Returns(appVersions.AsQueryable());

            var result = _appVersionService.VersionExists(0, "1.0");
            Assert.True(result);
        }
    }
}
