//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Danrevi.API.Models;

//namespace Danrevi.API.Datastore
//{
//    public class KursusDatastore
//    {
//        public static KursusDatastore Current { get; } = new KursusDatastore();
//        public List<Kursus> Kurser { get; set; }

//        public KursusDatastore()
//        {
//            Kurser = new List<Kursus>()
//            {
//                new Kursus()
//                {
//                    Id=1,
//                    Navn = "Moms kursus",
//                    Beskrivelse = "Kursus, der omhandler moms",
//                    Lokation = "Odense C",
//                    Arrangør = "Danrevi",
//                    Målgruppe = "Alle",
//                    Start = new DateTime(2019, 05, 11, 10, 30, 00),
//                    Slut = new DateTime(2019, 05, 11, 15, 30, 00)

//                },

//                new Kursus()
//                {
//                    Id=2,
//                    Navn = "Andre erklæringer end regnskabserklæringer",
//                    Beskrivelse = "Test",
//                    Lokation = "Middelfart",
//                    Arrangør = "Beierholm",
//                    Målgruppe = "Revisorer og assistenter på alle niveauer",
//                    Start = new DateTime(2019, 10, 08, 09, 00, 00),
//                    Slut = new DateTime(2019, 10, 08, 17, 00, 00)

//                },

//                new Kursus()
//                {
//                    Id=3,
//                    Navn = "Assistance og udvidet gennemgang",
//                    Beskrivelse = "Test",
//                    Lokation = "Fredericia",
//                    Arrangør = "BDO",
//                    Målgruppe = "Revisorer og assistenter på alle niveauer",
//                    Start = new DateTime(2019, 11, 13, 08, 30, 00),
//                    Slut = new DateTime(2019, 11, 13, 16, 00, 00)

//                }
//            };
//        }
//    }
//}
