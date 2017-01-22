using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTGLibrary.Data.Repositories;

namespace MTGLibrary.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ISetRepository SetRepository { get; }
        ICardRepository CardRepository { get; }
        IRarityRepository RarityRepository { get; }

        void Commit();
    }
}
