using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Repository.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EnterpriseMagnetDbContext _appContext;
        private readonly DbSet<T> _dbset;

        public GenericRepository(EnterpriseMagnetDbContext appContext)
        {
            _appContext = appContext;
            _dbset = _appContext.Set<T>();
        }

        public async Task<bool> AddAsync(Expression<Func<T, bool>> expression)
        { 
            throw new NotImplementedException();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }
    }
}
