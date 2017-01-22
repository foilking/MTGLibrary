using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Models.Import
{
    public class ImportSet
    {
        public string name { get; set; }
        public string code { get; set; }
        public string gathererCode { get; set; }
        public string magicCardsInfoCode { get; set; }
        public string releaseDate { get; set; }
        public string border { get; set; }
        public string type { get; set; }
        public object[] booster { get; set; }
        public string mkm_name { get; set; }
        public int mkm_id { get; set; }
        public ImportCard[] cards { get; set; }
        public ImportTranslations translations { get; set; }
        public string[] magicRaritiesCodes { get; set; }
        public string block { get; set; }
        public string oldCode { get; set; }
        public bool onlineOnly { get; set; }
        public string[] alternativeNames { get; set; }
    }
}
