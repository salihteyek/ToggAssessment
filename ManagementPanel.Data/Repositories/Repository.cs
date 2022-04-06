﻿using ManagementPanel.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagementPanel.Data.Repositories
{
    public class Repository<Tentity> : IRepository<Tentity> where Tentity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<Tentity> _dbSet;

        public Repository(ManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task Remove(Tentity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<Tentity> Update(Tentity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
