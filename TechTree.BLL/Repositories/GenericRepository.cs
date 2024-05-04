using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;

namespace TechTree.BLL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void Add(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T entity) => _dbContext.Update(entity);

        public void Delete(T entity) => _dbContext.Remove<T>(entity);
    }
}
