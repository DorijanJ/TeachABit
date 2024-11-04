using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachABit.Model.Models.Forumi;

namespace TeachABit.Repository.Repositories.Objave
{
    public interface IObjaveRepository
    {
        public Task<Objava> CreateObjava(Objava objava);
        public Task<Objava> UpdateObjava(Objava objava);
        public Task DeleteObjava(int id);
        public Task<List<Objava>> GetObjavaList();
        public Task<Objava?> GetObjavaById(int id);
    }
}
