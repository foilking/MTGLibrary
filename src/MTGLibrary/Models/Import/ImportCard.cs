using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Models.Import
{
    public class ImportCard
    {
        public string artist { get; set; }
        public float cmc { get; set; }
        public string[] colorIdentity { get; set; }
        public string[] colors { get; set; }
        public string flavor { get; set; }
        public ImportForeignName[] foreignNames { get; set; }
        public string id { get; set; }
        public string imageName { get; set; }
        public string layout { get; set; }
        public ImportLegality[] legalities { get; set; }
        public string manaCost { get; set; }
        public string mciNumber { get; set; }
        public int multiverseid { get; set; }
        public string name { get; set; }
        public string power { get; set; }
        public string rarity { get; set; }
        public ImportRuling[] rulings { get; set; }
        public string[] subtypes { get; set; }
        public string text { get; set; }
        public string toughness { get; set; }
        public string type { get; set; }
        public string[] types { get; set; }
        public bool reserved { get; set; }
        public string[] supertypes { get; set; }
        public int[] variations { get; set; }
        public bool starter { get; set; }
        public string number { get; set; }
        public string releaseDate { get; set; }
        public string border { get; set; }
        public int loyalty { get; set; }
        public int hand { get; set; }
        public int life { get; set; }
        public string[] names { get; set; }
        public string watermark { get; set; }
        public bool timeshifted { get; set; }
    }
}
