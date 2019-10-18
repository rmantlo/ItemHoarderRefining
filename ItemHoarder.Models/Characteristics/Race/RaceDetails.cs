using ItemHoarder.Data;
using ItemHoarder.Data.Characteristics.Features;
using ItemHoarder.Data.Characteristics.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characteristics.Race
{
    public class RaceDetails
    {
        public int RaceID { get; set; }
        public Guid OwnerID { get; set; }
        public GameType GameType { get; set; }
        public string RaceName { get; set; }
        public RaceType RaceType { get; set; }
        public string PhysicalDescription { get; set; }
        public double BaseSpeed { get; set; }
        public string Size { get; set; }
        //language name, SRW (speak, read, write)
        public Dictionary<string, string> Languages { get; set; }
        public double StrengthModifier { get; set; }
        public double DexterityModifier { get; set; }
        public double ConstitutionModifier { get; set; }
        public double IntelligenceModifier { get; set; }
        public double WisdomModifier { get; set; }
        public double CharismaModifier { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
    public class DnDRaceDetails : RaceDetails
    {
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        public List<string> ToolProficiencies { get; set; }
        public Dictionary<int, string> RaceFeatures { get; set; }
    }
    public class PathfinderRaceDetails : RaceDetails
    {
        public int RacePoints { get; set; }
        public PowerLevel PowerLevel { get; set; }
        //index class object of features
        public Dictionary<RacialTraitType, Dictionary<int, string>> RacialTraits { get; set; }
    }
}
