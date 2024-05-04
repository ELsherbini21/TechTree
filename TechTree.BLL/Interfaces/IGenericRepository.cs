using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTree.DAL.Models;

namespace TechTree.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        void Add(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        void Update(T entity);

        void Delete(T entity);
    }
}
