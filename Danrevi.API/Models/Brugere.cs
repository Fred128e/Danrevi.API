using System;
using System.Collections.Generic;

namespace Danrevi.API.Models
{
    public partial class Brugere
    {
        public Brugere()
        {
            BrugerKurser = new HashSet<BrugerKurser>();
        }

        public string FirebaseUid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<BrugerKurser> BrugerKurser { get; set; }
    }
}
