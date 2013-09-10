using System.Linq;
using DefectLog.Core.Domain;
using DefectLog.Core.Models;

namespace DefectLog.Core.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        Category Get(int id);
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public IQueryable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category Get(int id)
        {
            return _repository.Get(id);
        }

        public void Insert(Category category)
        {
            _repository.Insert(category);
            _repository.Save();
        }

        public void Update(Category category)
        {
            _repository.Update(category);
            _repository.Save();
        }

        public void Delete(Category category)
        {
            _repository.Delete(category);
            _repository.Save();
        }
    }
}