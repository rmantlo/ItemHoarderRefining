using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Features
{
    public enum RacialTraitType
    {
        AbilityScoreRacialTrait,
        DefensiveRacialTrait,
        OffensiveRacialTrait,
        FeatAndSkillRacialTraits,
        MagicalRacialTraits,
        WeaknessRacialTraits,
        SensesRacialTraits,
        MovementRacialTraits,
        OtherRacialTraits,
        None,
        NA
    }
    public class RaceFeatures
    {
        [Key]
        public int RaceFeatureID { get; set; }
        [ForeignKey("Feature")]
        public int FeatureID { get; set; }
        public virtual Feature Feature { get; set; }
        public RacialTraitType TraitType { get; set; }
    }
}
