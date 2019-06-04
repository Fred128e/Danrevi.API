//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Danrevi.API.Models;

//namespace Danrevi.API.Datastore
//{
//    public class NyhederDatastore
//    {
//        public static NyhederDatastore Current { get; } = new NyhederDatastore();
//        public List<Nyheder> Nyhed { get; set; }

//        public NyhederDatastore()
//        {
//            Nyhed = new List<Nyheder>()
//            {
//                new Nyheder()
//                {
//                    Id=1,
//                    Titel = "FSR nyt",
//                    Forfatter = "Frederik N.",
//                    OprettelsesDato = new DateTime(2019, 05, 09),
//                    Indhold = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
//                    Tags = "FSR, Danrevi"
//                },

//                new Nyheder()
//                {
//                    Id=2,
//                    Titel = "Udlejning via Airbnb indberettes til skattemyndighederne",
//                    Forfatter = "Hans Hansen",
//                    OprettelsesDato = new DateTime(2019, 05, 11),
//                    Indhold = "Aftalen mellem de danske skattemyndigheder og Airbnb træder i kraft fra 1. juli 2019. Airbnb begynder som den første deleøkonomiske platform at dele oplysninger med skattemyndighederne om danskernes indtægter fra udlejning.",
//                    Tags = "AirBnb, Skat"
//                },

//                new Nyheder()
//                {
//                    Id=3,
//                    Titel = "Rasmus Paludan bliver topnavn på Roskilde Festival",
//                    Forfatter = "Jens Jensen",
//                    OprettelsesDato = new DateTime(2019, 06, 01),
//                    Indhold = "Efter kritik af et middelmådigt program har Roskilde Festival annonceret, at det provokerende performanceband Stram Kurs med Rasmus Paludan i spidsen bliver hovednavn på Orange Scene.",
//                    Tags = "Rasmus Paludan, Roskilde"
//                },
//            };
//        }
//    }
//}
