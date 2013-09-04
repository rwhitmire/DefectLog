using System;
using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;

namespace DefectLog.Services
{
    public interface IDefectService
    {
        IQueryable<Defect> GetAll();
        IQueryable<Defect> GetAll(int versionId);
        Defect Get(int id);
        void Update(Defect defect);
        void Insert(Defect defect);
    }

    public class DefectService : IDefectService
    {
        private readonly IRepository<Defect> _repository;
        private readonly IRepository<Comment> _commentRepository;

        public DefectService(IRepository<Defect> repository, IRepository<Comment> commentRepository)
        {
            _repository = repository;
            _commentRepository = commentRepository;
        }

        public IQueryable<Defect> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Defect> GetAll(int versionId)
        {
            return _repository.GetAll().Where(x => x.AppVersionId == versionId);
        }

        public Defect Get(int id)
        {
            return _repository.Get(id);
        }

        public void Update(Defect defect)
        {
            _repository.Update(defect);
            
            if (defect.Comments.Any())
            {
                var comment = defect.Comments.First();
                _commentRepository.Insert(comment);
            }

            _repository.Save();
        }

        public void Insert(Defect defect)
        {
            defect.DateLogged = DateTime.Now;

            _repository.Insert(defect);
            _repository.Save();
        }
    }
}