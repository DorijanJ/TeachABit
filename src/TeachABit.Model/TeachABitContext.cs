using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.Models.Forumi;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Model.Models.User;

namespace TeachABit.Model
{
    public class TeachABitContext(DbContextOptions options) : IdentityDbContext<Korisnik>(options)
    {
        public DbSet<Tecaj> Tecajevi { get; set; }
        public DbSet<Objava> Objave { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
