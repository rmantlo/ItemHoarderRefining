using System;
using System.Collections.Generic;
using ItemHoarder.Data;
using ItemHoarder.Data.Characteristics;
using ItemHoarder.Data.Characteristics.Races;
using ItemHoarder.Models.Characteristics.Race;
using ItemHoarder.Services.Characteristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemHoarder.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RaceAndPolymorphism()
        {
            List<Race> some = new List<Race>();
            DnDRace race = new DnDRace();
            race.ArmorProficiencies = "blah";
            race.RaceName = "name";
            PathfinderRace race2 = new PathfinderRace();

            some.Add(race);
            some.Add(race2);

            Assert.AreEqual(2, some.Count);
            //using( var ctx = new ApplicationDbContext())
            //{
            //    ctx.Races.Add(race);
            //    ctx.SaveChanges();
            //}
        }
        [TestMethod]
        public void TestTest()
        {
            RaceService service = new RaceService(Guid.Parse("00000000-0000-0000-0000-000000000000"));
            DnDRaceCreate something = new DnDRaceCreate();
            something.WeaponProficiencies = new List<string>{"AHHH"};
            //service.GetAllMyRaces();
            service.CreateRace(something);
        }
    }
}
