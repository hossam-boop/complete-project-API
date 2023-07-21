using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _Context;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext Context)
        {
            _Context = Context;
        }
        public async Task<int> Complete()
            => await _Context.SaveChangesAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;//"product"

            if (!_repositories.Contains(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_Context);

                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}
