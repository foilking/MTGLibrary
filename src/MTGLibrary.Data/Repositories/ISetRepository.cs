using MTGLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Repositories
{
    public interface ISetRepository
    {
        IEnumerable<Set> GetAll();
        Set Find(string code);
        void Add(Set set);
        void Update(Set set);
        void Remove(string code);
    }
}
