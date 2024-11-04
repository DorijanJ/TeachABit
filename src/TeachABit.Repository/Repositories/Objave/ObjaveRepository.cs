using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachABit.Model;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Repository.Repositories.Objave
{
    public class ObjaveRepository(TeachABitContext context) : IObjaveRepository
    {
        private readonly TeachABitContext _context = context;
        public async Task<Objava> CreateObjava(Objava objava)
        {
            var createdObjava = await _context.Objave.AddAsync(objava);
            await _context.SaveChangesAsync();
            return createdObjava.Entity;
        }

        public async Task DeleteObjava(int id)
        {
            await _context.Objave.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public Task<Objava?> GetObjavaById(int id)
        {
            return _context.Objave.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Objava>> GetObjavaList()
        {
            return _context.Objave.ToListAsync();
        }

        public Task<Objava> UpdateObjava(Objava objava)
        {
            // on wait
            throw new NotImplementedException();
        }
    }
}
