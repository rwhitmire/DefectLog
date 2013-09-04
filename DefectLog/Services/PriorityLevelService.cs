using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;

namespace DefectLog.Services
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