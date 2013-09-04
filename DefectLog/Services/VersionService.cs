using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;

namespace DefectLog.Services
{
    public interface IVersionService
    {
        IQueryable<AppVersion> GetAll();
    }

    public class VersionService : IVersionService
    {
        private readonly IRepository<AppVersion> _repository;

        public VersionService(IRepository<AppVersion> repository)
        {
            _repository = repository;
        }

        public IQueryable<AppVersion> GetAll()
        {
            return _repository.GetAll();
        }
    }
}