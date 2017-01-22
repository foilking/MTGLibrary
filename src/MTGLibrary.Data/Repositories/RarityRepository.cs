using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MTGLibrary.Data.Models;
using Dapper;

namespace MTGLibrary.Data.Repositories
{
    internal class RarityRepository : RepositoryBase, IRarityRepository
    {
        public RarityRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Rarity Find(string name)
        {
            return Connection.Query<Rarity>("SELECT * FROM [Rarity] WHERE [Name] = @Name", new { Name = name }, transaction: Transaction).SingleOrDefault();
        }

        public IEnumerable<Rarity> GetAll()
        {
            return Connection.Query<Rarity>("SELECT * FROM [Rarity]", transaction: Transaction);
        }
    }
}
