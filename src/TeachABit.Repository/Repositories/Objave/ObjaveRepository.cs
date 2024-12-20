﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Objava?> GetObjavaById(int id)
        {
            return await _context.Objave
                .Include(x => x.Vlasnik)
                .Include(x => x.Komentari)
                .ThenInclude(x => x.Vlasnik)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Objava>> GetObjavaList(string? search)
        {
            IQueryable<Objava> objaveQuery = _context.Objave
             .Include(x => x.Vlasnik)
             .AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                objaveQuery = objaveQuery.Where(x => x.Naziv.ToLower().Contains(lowerSearch));
            }

            return await objaveQuery.ToListAsync();
        }

        public Task<Objava> UpdateObjava(Objava objava)
        {
            // on wait
            throw new NotImplementedException();
        }
    }
}
