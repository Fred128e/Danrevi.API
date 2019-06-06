using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class BrugerKurser
    {
        public int KursusId { get; set; }
        public string Uid { get; set; }

        public Kurser Kursus { get; set; }
        public Brugere U { get; set; }
    }
}
