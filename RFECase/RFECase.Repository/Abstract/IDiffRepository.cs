using RFECase.Domain.Base.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFECase.Repository.Abstract
{
    public interface IDiffRepository<T> where T:BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        bool IsExist(int id);
    }
}
