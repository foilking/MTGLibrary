using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Models
{
    public class Ruling
    {
        public int Id { get; set; }
        public virtual Card Card { get; set; }
        public int CardId {
            get
            {
                return Card.Id;
            }
        }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
