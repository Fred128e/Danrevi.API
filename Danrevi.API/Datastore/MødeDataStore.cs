//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Danrevi.API.Models;

//namespace Danrevi.API.Datastore
//{
//    public class MødeDataStore
//    {

//        public static MødeDataStore Current { get; } = new MødeDataStore();
//        public List<Møde> Møder { get; set; }

//        public MødeDataStore()  
//        {
//            Møder = new List<Møde>()
//            {
//                new Møde()
//                {
//                    Id=1,
//                    Navn = "Sommerkursus",
//                    Beskrivelse = "Sommemøde for alle Danrevis medlemmer",
//                    Lokation = "Fredericia ",
//                    Start = new DateTime(2019, 06, 06, 10, 00, 00),
//                    Slut = new DateTime(2019, 05, 11, 16, 30, 00)

//                },

//                new Møde()
//                {
//                    Id=2,
//                    Navn = "Årsmøde",
//                    Beskrivelse = "Årsmøde for alle Danrevis medlemmer",
//                    Lokation = "Fredericia",
//                    Start = new DateTime(2019, 12, 01, 09, 00, 00),
//                    Slut = new DateTime(2019, 10, 08, 15, 00, 00)

//                },

//                new Møde()
//                {
//                    Id=3,
//                    Navn = "Kvartalmøde",
//                    Beskrivelse = "Test",
//                    Lokation = "Fredericia",
//                    Start = new DateTime(2020, 01, 13, 08, 30, 00),
//                    Slut = new DateTime(2020, 01, 13, 16, 00, 00)

//                },
//                new Møde()
//                {
//                    Id=4,
//                    Navn = "Møde for nye medlemmer",
//                    Beskrivelse = "Kun for nye medlemmer",
//                    Lokation = "Fredericia",
//                    Start = new DateTime(2020, 05, 08, 9, 00, 00),
//                    Slut = new DateTime(2020, 05, 08, 15, 00, 00)

//                }
//            };
//        }
//    }
//}
