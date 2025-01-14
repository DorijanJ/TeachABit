using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Model
{
    public class TeachABitContext(DbContextOptions options) : IdentityDbContext<Korisnik, IdentityRole, string>(options)
    {
        public DbSet<Tecaj> Tecajevi { get; set; }
        public DbSet<Objava> Objave { get; set; }
        public DbSet<Radionica> Radionice { get; set; }
        public DbSet<Komentar> Komentari { get; set; }
        public DbSet<Lekcija> Lekcije { get; set; }
        public DbSet<ObjavaReakcija> ObjavaReakcije { get; set; }
        public DbSet<KomentarReakcija> KomentarReakcije { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
