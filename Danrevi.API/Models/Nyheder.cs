using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class Nyheder
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Forfatter { get; set; }
        public DateTime OprettelsesDato { get; set; }
        public string Indhold { get; set; }
    }
}
