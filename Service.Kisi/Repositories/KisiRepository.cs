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
    }
}
