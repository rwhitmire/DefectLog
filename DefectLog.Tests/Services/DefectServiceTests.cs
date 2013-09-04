using System;
using DefectLog.Domain;
using DefectLog.Models;
using DefectLog.Services;
using Moq;
using Xunit;

namespace DefectLog.Tests.Services
{
    public class DefectServiceTests
    {
        private readonly Mock<IRepository<Defect>> _defectRepository;
        private readonly Mock<IRepository<Comment>> _commentRepository;
        private readonly DefectService _service;

        public DefectServiceTests()
        {
            _defectRepository = new Mock<IRepository<Defect>>();
            _commentRepository = new Mock<IRepository<Comment>>(); 
            _service = new DefectService(_defectRepository.Object, _commentRepository.Object);
        }

        [Fact]
        public void InsertShouldOverrideGivenDate()
        {
            var defect = new Defect
            {
                DateLogged = new DateTime(2010, 1, 1)
            };

            _defectRepository.Setup(x => x.Insert(It.IsAny<Defect>())).Callback<Defect>(d => defect = d);
            _service.Insert(defect);

            Assert.Equal(defect.DateLogged.Year, DateTime.Now.Year);
        }
    }
}
