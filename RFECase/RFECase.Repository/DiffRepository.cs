using RFECase.Domain.Base.Entities;
using RFECase.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFECase.Repository
{
    public class DiffRepository<T> : IDiffRepository<T> where T : BaseEntity
    {
        private List<T> entities;

        public DiffRepository()
        {
            entities = new List<T>();
        }
        public Task Add(T entity)
        {
            entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var entity =  entities.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            entity.IsDeleted = true;

            return Task.CompletedTask;
        }

        public Task<T> Get(int id)
        {
            var entity = entities.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            var result = entities.Where(x => !x.IsDeleted);
            return Task.FromResult(result);
        }

        public bool IsExist(int id)
        {
            return entities.Any(x => x.Id == id && !x.IsDeleted);
        }

        public Task Update(T entity)
        {
            var existedEntity = entities.FirstOrDefault(x => x.Id == entity.Id && !x.IsDeleted);
            existedEntity = entity;

            return Task.CompletedTask;
        }
    }
}
