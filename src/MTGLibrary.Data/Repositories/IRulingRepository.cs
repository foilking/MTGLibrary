using MTGLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Repositories
{
    public interface IRulingRepository
    {
        void Add(Ruling ruling);
        void Update(Ruling ruling);
        void Remove(Ruling ruling);
    }
}
