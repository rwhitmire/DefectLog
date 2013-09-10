using System.Linq;
using DefectLog.Core.Domain;
using DefectLog.Core.Models;

namespace DefectLog.Core.Services
{
    public interface IPriorityLevelService
    {
        IQueryable<PriorityLevel> GetAll();
    }

    public class PriorityLevelService : IPriorityLevelService
    {
        private readonly IRepository<PriorityLevel> _repository;

        public PriorityLevelService(IRepository<PriorityLevel> repository)
        {
            _repository = repository;
        }

        public IQueryable<PriorityLevel> GetAll()
        {
            return _repository.GetAll();
        }
    }
}