﻿using ManagementPanel.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ManagementPanel.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public UnitOfWork(ManagementDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
