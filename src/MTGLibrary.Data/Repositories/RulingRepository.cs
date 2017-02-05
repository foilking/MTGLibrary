using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MTGLibrary.Data.Models;
using Dapper;

namespace MTGLibrary.Data.Repositories
{
    internal class RulingRepository : RepositoryBase, IRulingRepository
    {
        public RulingRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(Ruling ruling)
        {
            try
            {
                string query = "INSERT INTO [Ruling] ([CardId], [Date], [Text])"
                + " Values(@CardId, @Date, @Text)";

                Connection.Execute(query, ruling, transaction: Transaction);
            }
            catch (Exception e)
            {
                var error = e.Data;
            }
        }

        public void Remove(Ruling ruling)
        {
            Connection.Execute("DELETE FROM [Ruling] WHERE [Id] = @Id", ruling, transaction: Transaction);
        }

        public void Update(Ruling ruling)
        {
            Connection.Execute("UPDATE [Ruling] SET [CardId] = @CardId, [Date] = @Date, [Text] = @Text WHERE Id = @Id", ruling, transaction: Transaction);
        }
    }
}
