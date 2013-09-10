using System.Linq;
using DefectLog.Core.Domain;
using DefectLog.Core.Models;

namespace DefectLog.Core.Services
{
    public interface IAppVersionService
    {
        IQueryable<AppVersion> GetAll();
        AppVersion Get(int id);
        void Insert(AppVersion appVersion);
        void Update(AppVersion appVersion);
        void Delete(AppVersion appVersion);
        bool VersionExists(int id, string versionNumber);
    }

    public class AppVersionService : IAppVersionService
    {
        private readonly IRepository<AppVersion> _repository;

        public AppVersionService(IRepository<AppVersion> repository)
        {
            _repository = repository;
        }

        public IQueryable<AppVersion> GetAll()
        {
            return _repository.GetAll();
        }

        public AppVersion Get(int id)
        {
            return _repository.Get(id);
        }

        public void Insert(AppVersion appVersion)
        {
            _repository.Insert(appVersion);
            _repository.Save();
        }

        public void Update(AppVersion appVersion)
        {
            _repository.Update(appVersion);
            _repository.Save();
        }

        public void Delete(AppVersion appVersion)
        {
            _repository.Delete(appVersion);
            _repository.Save();
        }

        public bool VersionExists(int id, string versionNumber)
        {
            return _repository.GetAll()
                .Where(x => x.Id != id)
                .Any(x => x.VersionNumber == versionNumber);
        }
    }
}