using System.Linq;
using DefectLog.Core.Domain;
using DefectLog.Core.Models;

namespace DefectLog.Core.Services
{
    public interface IStatusService
    {
        IQueryable<Status> GetAll();
    }

    public class StatusService : IStatusService
    {
        private readonly IRepository<Status> _repository;

        public StatusService(IRepository<Status> repository)
        {
            _repository = repository;
        }

        public IQueryable<Status> GetAll()
        {
            return _repository.GetAll();
        }
    }
}