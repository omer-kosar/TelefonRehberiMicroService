﻿using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Kisi.Repositories.Interfaces;

namespace Service.Kisi.Repositories
{
    public class KisiRepository : IKisiRepository
    {
        protected KisiContext _context;
        public KisiRepository(KisiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task KisiKaydet(Entities.Kisi kisi)
        {
            _context.Kisi.Add(kisi);
            await _context.SaveChangesAsync();
        }
        public async Task<Entities.Kisi> GetirKisiById(Guid id)
        {
            return await _context.Kisi.FindAsync(id);
        }

        public async Task<IEnumerable<Entities.Kisi>> GetirKisiListesi()
        {
            return await _context.Kisi.ToListAsync();
        }

        public async Task KisiSil(Guid id)
        {
            var deletedKisi = await _context.Kisi.FindAsync(id);
            _context.Kisi.Remove(deletedKisi);
            await _context.SaveChangesAsync();
        }
    }
}
