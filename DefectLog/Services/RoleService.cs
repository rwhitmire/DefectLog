using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;

namespace DefectLog.Services
{
    public interface IRoleService
    {
        IQueryable<Role> GetAll();
    }

    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;

        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
        }

        public IQueryable<Role> GetAll()
        {
            return _repository.GetAll();
        }
    }
}