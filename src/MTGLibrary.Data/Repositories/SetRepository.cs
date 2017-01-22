using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTGLibrary.Data.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MTGLibrary.Data.Repositories
{
    internal class SetRepository : RepositoryBase, ISetRepository
    {
        public SetRepository (IDbTransaction transaction)
            : base (transaction)
        {
            
        }

        public void Add(Set set)
        {
            try
            {
                string query = "INSERT INTO [Set] (Name, Code, GathererCode, ReleaseDate, Border, Type)"
                        + " VALUES(@Name, @Code, @GathererCode, @ReleaseDate, @Border, @Type)";

                Connection.Execute(query, set, transaction: Transaction);
            }
            catch (Exception e)
            {
                var error = e.Data;
            }
        }

        public Set Find(string code)
        {
            return Connection.Query<Set>("SELECT * FROM [Set] WHERE [Code] = @Code", new { Code = code }, transaction: Transaction).SingleOrDefault();
        }

        public IEnumerable<Set> GetAll()
        {
            return Connection.Query<Set>("SELECT * FROM [Set]", transaction: Transaction);
        }

        public void Remove(string code)
        {
            Connection.Execute("DELETE FROM [Set] WHERE [Code] = @Code", new { Code = code }, transaction: Transaction);
        }

        public void Update(Set set)
        {
            Connection.Execute("UPDATE [Set] SET Name = @Name, Code = @Code, ReleaseDate = @ReleaseDate, Border = @Border, Type = @Type WHERE Id = @Id", set, transaction: Transaction);
        }
    }
}
