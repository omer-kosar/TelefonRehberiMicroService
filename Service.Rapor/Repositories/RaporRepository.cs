using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Rapor.Repositories.Interfaces;

namespace Service.Rapor.Repositories
{
    public class RaporRepository : IRaporRepository
    {
        protected RaporContext _context;

        public RaporRepository(RaporContext context)
        {
            _context = context;
        }

        public async Task<Entities.Rapor> GetirRaporById(Guid id)
        {
            return await _context.Rapor.FindAsync(id);

        }
    }
}
