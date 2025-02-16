﻿using Microsoft.EntityFrameworkCore;
using UserPanel.Core.UnitOfWork;

namespace UserPanel.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
