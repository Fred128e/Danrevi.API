﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danrevi.API.Models
{
    public class KursusOversigt
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokation { get; set; }
        public string Arrangør { get; set; }
        public string Målgruppe { get; set; }
        public DateTime Start { get; set; }
        public DateTime slut { get; set; }
        public string Email { get; set; }

    }
}
