using EnterpriseMagnet.DataAccess.Abstract.UnitOfWorks;
using EnterpriseMagnet.Repository.Abstract;
using EnterpriseMagnet.Service.Abstract;
using EnterpriseMagnet.Service.Abstract.Services;
using EnterpriseMagnet.Service.Concrete.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Service.Concrete.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWorks _unitOfWorks;

        public Service(IGenericRepository<T> genericRepository, IUnitOfWorks unitOfWorks)
        {
            _genericRepository = genericRepository;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<bool> AddAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return await _genericRepository.AddAsync(expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWorks.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWorks.CommitAsync();
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _genericRepository.Delete(entity);
            _unitOfWorks.CommitAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _genericRepository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _genericRepository.GetByIdAsync(id);
            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return hasProduct;
        }

        public async Task UpdateAsync(T entity)
        {
            _genericRepository.Update(entity);
            await _unitOfWorks.CommitAsync();
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _genericRepository.Where(expression);
        }
    }
}
