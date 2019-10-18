using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Features
{
    public class Feature
    {
        [Key]
        public int FeatureID { get; set; }
        public Guid OwnerID { get; set; }
        public GameType GameType { get; set; }
        public int? RacePointCost { get; set; } //pathfinder
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
        public string Special { get; set; }
        public ICollection<FRacePrerequisites> RaceIDPrerequisites { get; set; } //may be multiples
        public string ClassIDPrerequisites { get; set; }
        public ICollection<FFeatPrerequisites> FeatureIDPrerequisites { get; set; }
        public string StatPrerequisite { get; set; }//Con +1|Str +9 etc
        public int? LvlPrerequisite { get; set; }//see above
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
}
