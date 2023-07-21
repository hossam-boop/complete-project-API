using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _Context;

        public GenericRepository(StoreDbContext Context)
        {
            _Context = Context;
        }
        public void Add(T entity) 
            => _Context.Set<T>().Add(entity);
       
        public void Delete(T entity)
           => _Context.Set<T>().Remove(entity);

        public async Task<T> GetByIdAsync(int id)
            => await _Context.Set<T>().FindAsync(id);

        public async Task<T> GetEntityWithSpecifications(ISpecifications<T> specifications)
                => await ApplySpecifications(specifications).FirstOrDefaultAsync();
        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _Context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specifications)
          => await ApplySpecifications(specifications).ToArrayAsync();

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
            => SpecificationsEvaluator<T>.GetQuery(_Context.Set<T>().AsQueryable(), specifications);

        public void Update(T entity)
           => _Context.Set<T>().Update(entity);

    }
}
