using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary.Data.Models
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string GathererCode { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Border { get; set; }
        public string Type { get; set; }
    }
}
