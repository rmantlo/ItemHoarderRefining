using ItemHoarder.Data.Characteristics.Features;
using ItemHoarder.Data.Characteristics.Races;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Races
{
    public enum RaceType
    {
        Aberration,
        Animal,
        Construct,
        Dragon,
        Fey,
        Humanoid,
        Magical_Beast,
        Monstrous_Humanoid,
        Ooze,
        Outsider,
        Plant,
        Undead,
        Non_Humanoid,
        Other
    }
    public enum PowerLevel
    {
        Standard, //3 each trait cat,
        Advanced, //4 each trait cat,
        Monstrous //5 each trait cat,
    }
    public class Race
    {

        [Key]
        public int RaceID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public GameType GameType { get; set; }
        public string RaceName { get; set; }
        public RaceType RaceType { get; set; }
        public string PhysicalDescription { get; set; }
        public double BaseSpeed { get; set; }
        public string Size { get; set; }
        public string Languages { get; set; }
        public double StrengthModifier { get; set; }
        public double DexterityModifier { get; set; }
        public double ConstitutionModifier { get; set; }
        public double IntelligenceModifier { get; set; }
        public double WisdomModifier { get; set; }
        public double CharismaModifier { get; set; }
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
    public class DnDRace : Race
    {
        public string WeaponProficiencies { get; set; }
        public string ArmorProficiencies { get; set; }
        public string ToolProficiencies { get; set; }
        public ICollection<RaceFeatures> RaceFeatures { get; set; }
    }
    public class PathfinderRace : Race
    {
        //pathfinder racial traits are split into: ability score, defense, feat and skill, magical, movement, offensive, senses, weakness, and other.
        //the number of traits you can have depends of power lvl of race.
        //standard picks 3, advanced gets 4, monstrous gets 5.
        public int RacePoints { get; set; }
        public PowerLevel PowerLevel { get; set; }
        public ICollection<RaceFeatures> RacialTraits { get; set; }
        //racial traits have:
        //prerequisites
        //benefits
        //special
        //RP cost
        //just make these features
    }
}
