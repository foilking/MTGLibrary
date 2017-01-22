using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int MultiverseId { get; set; }

        public string Name { get; set; }
        public string ManaCost { get; set; }
        public double Cmc { get; set; }
        public string Type { get; set; }
        public virtual Set Set { get; set; }
        public int SetId
        {
            get
            {
                return Set.Id;
            }
        }
        public virtual Rarity Rarity { get; set; }
        public int RarityId
        {
            get
            {
                return Rarity.Id;
            }
        }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string Artist { get; set; }
        public string Number { get; set; }

        public string Power { get; set; }
        public string Toughness { get; set; }
        public int Loyalty { get; set; }

        public string Layout { get; set; }
        public bool Reserved { get; set; }
        public bool Starter { get; set; }
        public bool Timeshifted { get; set; }

        public int[] Variations { get; set; }

        public string[] Supertypes { get; set; }
        public string[] Types { get; set; }
        public string[] Subtypes { get; set; }
        public string[] ColorIdentity { get; set; }
        public string[] Colors { get; set; }
    }
}
