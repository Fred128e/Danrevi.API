using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class Deadline
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public DateTime Dato { get; set; }
    }
}
