using MTGLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Repositories
{
    public interface IRarityRepository
    {
        Rarity Find(string name);
        IEnumerable<Rarity> GetAll();
    }
}
