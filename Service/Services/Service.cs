using Core.Repositorys.Interface;
using Core.Services.Interface;
using Core.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using System.Linq.Expressions;

namespace Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        public Service(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;  
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public  Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _genericRepository.GetAll().ToListAsync();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _genericRepository.GetBy(predicate);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct= await _genericRepository.GetByIdAsync(id);
            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(T).Name} Not Found");
            }
            return hasProduct;
        }

        public async Task RemoveAsync(T entity)
        {
            _genericRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            _genericRepository.RemoveRange(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _genericRepository.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
