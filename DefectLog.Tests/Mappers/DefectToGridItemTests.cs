using AutoMapper;
using DefectLog.Core.Models;
using DefectLog.Web.App_Start;
using DefectLog.Web.ViewModels;
using Xunit;

namespace DefectLog.Tests.Mappers
{
    public class DefectToGridItemTests
    {
        public DefectToGridItemTests()
        {
            MappingConfig.RegisterMappings();
        }

        [Fact]
        public void DeveloperName()
        {
            var defect = new Defect
            {
                Developer = new User
                {
                    FirstName = "Bill",
                    LastName = "Smith"
                }
            };

            var result = Mapper.Map<DefectListItem>(defect);

            Assert.Equal("B. Smith", result.DeveloperName);
        }

        [Fact]
        public void TesterName()
        {
            var defect = new Defect
            {
                Tester = new User
                {
                    FirstName = "Bill",
                    LastName = "Smith"
                }
            };

            var result = Mapper.Map<DefectListItem>(defect);

            Assert.Equal("B. Smith", result.TesterName);
        }

        [Fact]
        public void CssClass()
        {
            var defect = new Defect
            {
                Status = new Status
                {
                    CssClass = "info"
                }
            };

            var result = Mapper.Map<DefectListItem>(defect);

            Assert.Equal("info", result.CssClass);
        }
    }
}
