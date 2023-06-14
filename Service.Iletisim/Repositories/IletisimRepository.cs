﻿using Microsoft.EntityFrameworkCore;
using Service.Iletisim.Data;
using Service.Iletisim.Repositories.Interfaces;

namespace Service.Iletisim.Repositories
{
    public class IletisimRepository : IIletisimRepository
    {
        protected IletisimContext _context;

        public IletisimRepository(IletisimContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task IletisimKaydet(Entities.Iletisim iletisim)
        {
            _context.Iletisim.Add(iletisim);
            await _context.SaveChangesAsync();
        }
        public async Task<Entities.Iletisim> GetIletisimById(Guid id)
        {
            return await _context.Iletisim.FindAsync(id);
        }

        public async Task<IEnumerable<Entities.Iletisim>> GetirIletisimListesi()
        {
            return await _context.Iletisim.ToListAsync();
        }

        public async Task IletisimSil(Guid id)
        {
            var deletedIletisim = await _context.Iletisim.FindAsync(id);
            _context.Iletisim.Remove(deletedIletisim);
            await _context.SaveChangesAsync();
        }
    }
}
