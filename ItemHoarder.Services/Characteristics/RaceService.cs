using ItemHoarder.Data;
using ItemHoarder.Data.Characteristics.Features;
using ItemHoarder.Data.Characteristics.Races;
using ItemHoarder.Models.Characteristics.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Services.Characteristics
{
    public class RaceService
    {
        private readonly Guid _userId;
        public RaceService(Guid userId)
        {
            _userId = userId;
        }

        //get all my races
        public IEnumerable<RaceIndex> GetAllMyRaces()
        {
            using (var ctx = new ApplicationDbContext())
            {
                //took out where ownerId is == userId
                var results = ctx.Races.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new RaceIndex
                {
                    RaceID = e.RaceID,
                    OwnerID = e.OwnerID,
                    GameType = e.GameType,
                    RaceName = e.RaceName,
                    RaceType = e.RaceType,
                    PhysicalDescription = e.PhysicalDescription,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToArray();
                return results;
            }
        }
        //get races by room
        //public IEnumerable<RaceIndex> GetAllByRoom(int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var rooms = ctx.RoomRaces.Where(e => e.RoomID == roomId).ToList();
        //        List<RaceIndex> roomRaces = new List<RaceIndex>();
        //        foreach (var r in rooms)
        //        {
        //            var race = ctx.Races.Single(e => e.RaceID == r.RaceID);
        //            var raceDisplay = new RaceIndex
        //            {
        //                RaceID = race.RaceID,
        //                OwnerID = race.OwnerID,
        //                GameTag = race.GameTag,
        //                RaceName = race.RaceName,
        //                PhysicalDescription = race.PhysicalDescription,
        //                DateOfCreation = race.DateOfCreation,
        //                DateOfModification = race.DateOfModification
        //            };
        //            roomRaces.Add(raceDisplay);
        //        }

        //        return roomRaces;
        //    }
        //}


        //get race by id
        public RaceDetails GetRaceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var dbRace = ctx.Races.Where(e => e.RaceID == id).ToList();

                if (dbRace[0].GameType == GameType.DungeonsAndDragons)
                {
                    return DnDRaceDetailsSetup(dbRace.OfType<DnDRace>().Single());
                }
                else if (dbRace[0].GameType == GameType.Pathfinder)
                {
                    return PathfinderRaceDetailsSetup(dbRace.OfType<PathfinderRace>().Single());
                }
                else
                {
                    return null;
                }
            }
        }
        //create DND race
        public bool CreateRace(RaceCreate raceCreate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (raceCreate.GetType().Name == "DnDRaceCreate")
                {
                    var race = (DnDRaceCreate)raceCreate;
                    var newRace = new DnDRace
                    {
                        OwnerID = _userId,
                        RaceName = race.RaceName,
                        GameType = race.GameType,
                        RaceType = race.RaceType,
                        PhysicalDescription = race.PhysicalDescription,
                        BaseSpeed = race.BaseSpeed,
                        Size = race.Size,
                        Languages = String.Join("|", race.Languages.Select(l => l.Key + "*" + l.Value).ToArray()),
                        StrengthModifier = race.StrengthModifier,
                        DexterityModifier = race.DexterityModifier,
                        ConstitutionModifier = race.ConstitutionModifier,
                        IntelligenceModifier = race.IntelligenceModifier,
                        WisdomModifier = race.WisdomModifier,
                        CharismaModifier = race.CharismaModifier,
                        WeaponProficiencies = String.Join("|", race.WeaponProficiencies),
                        ArmorProficiencies = String.Join("|", race.ArmorProficiencies),
                        ToolProficiencies = String.Join("|", race.ToolProficiencies),
                        IsDeactivated = false,
                        DateOfCreation = DateTimeOffset.UtcNow
                    };
                    ctx.Races.Add(newRace);
                    return ctx.SaveChanges() == 1;
                }
                else if (raceCreate.GetType().Name == "PathfinderRaceCreate")
                {
                    var race = (PathfinderRaceCreate)raceCreate;
                    var newRace = new PathfinderRace
                    {
                        OwnerID = _userId,
                        RaceName = race.RaceName,
                        GameType = race.GameType,
                        RaceType = race.RaceType,
                        PhysicalDescription = race.PhysicalDescription,
                        BaseSpeed = race.BaseSpeed,
                        Size = race.Size,
                        Languages = String.Join("|", race.Languages.Select(l => l.Key + "*" + l.Value).ToArray()),
                        StrengthModifier = race.StrengthModifier,
                        DexterityModifier = race.DexterityModifier,
                        ConstitutionModifier = race.ConstitutionModifier,
                        IntelligenceModifier = race.IntelligenceModifier,
                        WisdomModifier = race.WisdomModifier,
                        CharismaModifier = race.CharismaModifier,
                        IsDeactivated = false,
                        RacePoints = race.RacePoints,
                        PowerLevel = race.PowerLevel,
                        DateOfCreation = DateTimeOffset.UtcNow
                    };
                    ctx.Races.Add(newRace);
                    foreach (var item in race.RacialTraits)
                    {
                        newRace.RacialTraits.Add(new RaceFeatures
                        {
                            FeatureID = item.Key,
                            TraitType = item.Value
                        });
                    }
                    var something = ctx.SaveChanges() == 1;
                    return something;
                }
                else
                {
                    return false;
                }
            }
        }
        //update race
        public bool UpdateRace(int id, RaceCreate race)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (race.GetType().Name == "DnDRaceCreate")
                {
                    var dndRace = (DnDRaceCreate)race;
                    var result = ctx.Races.OfType<DnDRace>().SingleOrDefault(e => e.OwnerID == _userId && e.RaceID == id);
                    if (result != null)
                    {
                        result.RaceName = race.RaceName;
                        result.GameType = race.GameType;
                        result.RaceType = race.RaceType;
                        result.PhysicalDescription = race.PhysicalDescription;
                        result.BaseSpeed = race.BaseSpeed;
                        result.Size = race.Size;
                        result.Languages = String.Join("|", race.Languages.Select(l => l.Key + "*" + l.Value).ToArray());
                        result.WeaponProficiencies = String.Join("|", dndRace.WeaponProficiencies);
                        result.ArmorProficiencies = String.Join("|", dndRace.ArmorProficiencies);
                        result.ToolProficiencies = String.Join("|", dndRace.ToolProficiencies);
                        result.StrengthModifier = race.StrengthModifier;
                        result.DexterityModifier = race.DexterityModifier;
                        result.ConstitutionModifier = race.ConstitutionModifier;
                        result.IntelligenceModifier = race.IntelligenceModifier;
                        result.WisdomModifier = race.WisdomModifier;
                        result.CharismaModifier = race.CharismaModifier;
                        result.DateOfModification = DateTimeOffset.UtcNow;
                        return ctx.SaveChanges() == 1;
                    }
                    else return false;
                }
                else if (race.GetType().Name == "PathfinderRaceCreate")
                {
                    var pathRace = (PathfinderRaceCreate)race;
                    var result = ctx.Races.OfType<PathfinderRace>().SingleOrDefault(e => e.OwnerID == _userId && e.RaceID == id);
                    if (result != null)
                    {
                        result.RaceName = race.RaceName;
                        result.GameType = race.GameType;
                        result.RaceType = race.RaceType;
                        result.PhysicalDescription = race.PhysicalDescription;
                        result.BaseSpeed = race.BaseSpeed;
                        result.Size = race.Size;
                        result.StrengthModifier = race.StrengthModifier;
                        result.DexterityModifier = race.DexterityModifier;
                        result.ConstitutionModifier = race.ConstitutionModifier;
                        result.IntelligenceModifier = race.IntelligenceModifier;
                        result.WisdomModifier = race.WisdomModifier;
                        result.CharismaModifier = race.CharismaModifier;
                        result.DateOfModification = DateTimeOffset.UtcNow;
                        result.Languages = String.Join("|", race.Languages.Select(l => l.Key + "*" + l.Value).ToArray());
                        result.RacePoints = pathRace.RacePoints;
                        result.PowerLevel = pathRace.PowerLevel;
                        result.RacialTraits.Clear();
                        foreach (var item in pathRace.RacialTraits)
                        {
                            result.RacialTraits.Add(new RaceFeatures
                            {
                                FeatureID = item.Key,
                                TraitType = item.Value
                            });
                        }
                        return ctx.SaveChanges() == 1;
                    }
                    else return false;
                }
                else return false;
            }
        }
        //delete race: just switch to deactivated = true
        public bool DeleteRace(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var roomRaces = ctx.RoomRaces.Where(e => e.OwnerID == _userId && e.RaceID == id).ToList();
                //foreach (var r in roomRaces)
                //{
                //    ctx.RoomRaces.Remove(r);
                //}
                var race = ctx.Races.Single(e => e.OwnerID == _userId && e.RaceID == id);
                race.IsDeactivated = true;
                return ctx.SaveChanges() == 1;
                //+ roomRaces.Count();
                //return false;
            }
        }


        private DnDRaceDetails DnDRaceDetailsSetup(DnDRace race)
        {
            Dictionary<string, string> languages = new Dictionary<string, string>();
            foreach (var language in race.Languages.Split('|').ToList())
            {
                var lang = language.Split('*').ToList();
                languages.Add(lang[0], lang[1]);
            }
            Dictionary<int, string> features = RaceFeaturesToDictionary(race.RaceFeatures);
            var raceDisplay = new DnDRaceDetails
            {
                RaceID = race.RaceID,
                OwnerID = race.OwnerID,
                GameType = race.GameType,
                RaceName = race.RaceName,
                RaceType = race.RaceType,
                PhysicalDescription = race.PhysicalDescription,
                BaseSpeed = race.BaseSpeed,
                Size = race.Size,
                Languages = languages,
                StrengthModifier = race.StrengthModifier,
                DexterityModifier = race.DexterityModifier,
                ConstitutionModifier = race.ConstitutionModifier,
                IntelligenceModifier = race.IntelligenceModifier,
                WisdomModifier = race.WisdomModifier,
                CharismaModifier = race.CharismaModifier,
                DateOfCreation = race.DateOfCreation,
                DateOfModification = race.DateOfModification,
                WeaponProficiencies = race.WeaponProficiencies.Split('|').ToList(),
                ArmorProficiencies = race.ArmorProficiencies.Split('|').ToList(),
                ToolProficiencies = race.ToolProficiencies.Split('|').ToList(),
                RaceFeatures = features
            };
            return raceDisplay;
        }
        private PathfinderRaceDetails PathfinderRaceDetailsSetup(PathfinderRace race)
        {
            Dictionary<string, string> languages = new Dictionary<string, string>();
            foreach (var language in race.Languages.Split('|').ToList())
            {
                var lang = language.Split('*').ToList();
                languages.Add(lang[0], lang[1]);
            }
            Dictionary<RacialTraitType, Dictionary<int, string>> racialTraits = new Dictionary<RacialTraitType, Dictionary<int, string>>();
            foreach (var item in race.RacialTraits)
            {
                if (!racialTraits.ContainsKey(item.TraitType))
                {
                    racialTraits.Add(item.TraitType, RaceFeaturesToDictionary(race.RacialTraits.Where(r => r.TraitType == item.TraitType).ToList()));
                }
            }
            var raceDisplay = new PathfinderRaceDetails
            {
                RaceID = race.RaceID,
                OwnerID = race.OwnerID,
                GameType = race.GameType,
                RaceName = race.RaceName,
                RaceType = race.RaceType,
                PhysicalDescription = race.PhysicalDescription,
                BaseSpeed = race.BaseSpeed,
                Size = race.Size,
                Languages = languages,
                StrengthModifier = race.StrengthModifier,
                DexterityModifier = race.DexterityModifier,
                ConstitutionModifier = race.ConstitutionModifier,
                IntelligenceModifier = race.IntelligenceModifier,
                WisdomModifier = race.WisdomModifier,
                CharismaModifier = race.CharismaModifier,
                DateOfCreation = race.DateOfCreation,
                DateOfModification = race.DateOfModification,
                RacePoints = race.RacePoints,
                PowerLevel = race.PowerLevel,
                RacialTraits = racialTraits,
            };
            return raceDisplay;
        }
        private Dictionary<int, string> RaceFeaturesToDictionary(IEnumerable<RaceFeatures> features)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (var trait in features)
            {
                result.Add(trait.FeatureID, trait.Feature.FeatureName);
            }
            return result;
        }

    }
}
