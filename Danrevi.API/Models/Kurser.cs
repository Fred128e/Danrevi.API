using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class Kurser
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokation { get; set; }
        public string Arrangør { get; set; }
        public string Målgruppe { get; set; }
        public DateTime Start { get; set; }
        public DateTime Slut { get; set; }
    }
}
