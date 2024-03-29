﻿using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class Møder
    {
        public Møder()
        {
            MøderBruger = new HashSet<MøderBruger>();
        }

        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokation { get; set; }
        public DateTime Start { get; set; }
        public DateTime Slut { get; set; }

        public ICollection<MøderBruger> MøderBruger { get; set; }
    }
}
