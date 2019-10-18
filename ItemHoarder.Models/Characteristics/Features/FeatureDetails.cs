using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characteristics.Features
{
    public class FeatureDetails
    {
        public int FeatureID { get; set; }
        public GameType GameType { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
        public string Special { get; set; }
        public int? RacePointCost { get; set; }
        //race ID, race name
        public Dictionary<int, string> RaceIdPrerequisite { get; set; }
        public Dictionary<int, string> ClassIdPrerequisite { get; set; }
        public Dictionary<int, string> FeatureIdPrerequisite { get; set; }
        public Dictionary<string, int> StatPrerequisite { get; set; }
        public int? LvlPrerequisite { get; set; }
        public double StrengthModifier { get; set; }
        public double DexterityModifier { get; set; }
        public double ConstitutionModifier { get; set; }
        public double IntelligenceModifier { get; set; }
        public double WisdomModifier { get; set; }
        public double CharismaModifier { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
