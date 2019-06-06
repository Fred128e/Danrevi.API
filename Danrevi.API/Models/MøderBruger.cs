using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class MøderBruger
    {
        public int MødeId { get; set; }
        public string Uid { get; set; }

        public Møder Møde { get; set; }
        public Brugere U { get; set; }
    }
}
