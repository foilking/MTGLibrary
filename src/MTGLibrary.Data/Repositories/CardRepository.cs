using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTGLibrary.Data.Models;
using System.Data;
using Dapper;

namespace MTGLibrary.Data.Repositories
{
    internal class CardRepository : RepositoryBase, ICardRepository
    {
        public CardRepository(IDbTransaction transaction)
            : base (transaction)
        {

        }

        public void Add(Card card)
        {
            try
            {
                string query = "INSERT INTO [Card] ([MultiverseId], [Name], [ManaCost], [Cmc], [Type], [RarityId], [SetId], [Text], [Flavor], [Artist], [Power], [Toughness], [Layout], [Reserved], [Starter], [Number], [Loyalty], [Timeshifted])"
                + " Values(@MultiverseId, @Name, @ManaCost, @Cmc, @Type, @RarityId, @SetId, @Text, @Flavor, @Artist, @Power, @Toughness, @Layout, @Reserved, @Starter, @Number, @Loyalty, @Timeshifted)";

                Connection.Execute(query, card, transaction: Transaction);
            }
            catch (Exception e)
            {
                var error = e.Data;
            }
        }

        public bool DoesCardExist(string name, Set set)
        {
            var query =  "SELECT COUNT(*) FROM [Card]"
                + " WHERE [Card].[Name] = @Name"
                + " AND [Card].[SetId] = @SetId";

            try
            {
                var results = Connection.ExecuteScalar<int>(query, new { Name = name, SetId = set.Id }, transaction: Transaction);

                return results > 0;
            }
            catch (Exception e)
            {
                // This is not the right way to do this, but I can't figure out the proper solution yet

                return false;
            }

        }

        public IEnumerable<Card> Find(string name = null, int? setId = null, int? multiverseId = null)
        {
            var parameter = new DynamicParameters();
            var query = "SELECT [Card].*, [Set].*, [Rarity].* FROM [Card]"
                + "INNER JOIN [Set] ON [Card].[SetId] = [Set].[Id]"
                + "INNER JOIN [Rarity] ON [Card].[RarityId] = [Rarity].[Id]"
                + "WHERE 1 = 1";
            if (!string.IsNullOrWhiteSpace(name))
            {
                query += " AND [Card].[Name] = @Name";
                parameter.Add("Name", name);
            }
            if (setId.HasValue)
            {
                query += " AND [Card].[SetId] = @SetId";
                parameter.Add("SetId", setId.Value);
            }
            if (multiverseId.HasValue)
            {
                query += " AND [Card].[MultiverseId] = @MultiverseId";
                parameter.Add("MultiverseId", multiverseId.Value);
            }
            return Connection.Query<Card, Set, Rarity, Card>(query,
                (card, set, rarity) =>
                {
                    if (card != null)
                    {
                        card.Set = set;
                        card.Rarity = rarity;
                    }
                    return card;
                }, parameter, transaction: Transaction);
        }

        public void Remove(Card card)
        {
            Connection.Execute("DELETE FROM [Card] WHERE [Id] = @Id", card, transaction: Transaction);
        }

        public void Update(Card card)
        {
            Connection.Execute("UPDATE [Card] SET MultiverseId = @MultiverseId, Name = @Name, ManaCost = @ManaCost, Cmc = @Cmc, Type = @Type, RarityId = @RarityId, SetId = @SetId, Text = @Text, Flavor = @Flavor, Artist = @Artist, Power = @Power, Toughness = @Toughness, Layout = @Layout, Reserved = @Reserved, Starter = @Starter, Number = @Number, Loyalty = @Loyalty, Timeshifted = @Timeshifted WHERE Id = @Id", card, transaction: Transaction);
        }
    }
}
