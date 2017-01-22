using MTGLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Repositories
{
    public interface ICardRepository
    {
        IEnumerable<Card> Find(string name = null, int? setId = null, int? multiverseId = null);
        void Add(Card card);
        void Update(Card card);
        void Remove(Card card);
        bool DoesCardExist(string name, Set set);
    }
}
