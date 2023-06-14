using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Rapor.Entities;
using Service.Rapor.Repositories.Interfaces;

namespace Service.Rapor.Repositories
{
    public class RaporBilgiRepository : IRaporBilgiRepository
    {
        protected RaporContext _context;
        public RaporBilgiRepository(RaporContext context)
        {
            _context = context;
        }
        public async Task<RaporBilgi> GetirRaporBilgiById(Guid id)
        {
            return await _context.RaporBilgi.FindAsync(id);
        }

        public async Task<IEnumerable<RaporBilgi>> GetirRaporDetayBilgiList(Guid raporId)
        {
            return await _context.RaporBilgi.Where(i => i.RaporId == raporId).ToListAsync();
        }

        public async Task RaporBilgiKaydet(RaporBilgi raporBilgi)
        {
            _context.RaporBilgi.Add(raporBilgi);
            await _context.SaveChangesAsync();
        }
    }
}
