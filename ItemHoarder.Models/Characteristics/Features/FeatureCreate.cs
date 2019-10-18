using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characteristics.Features
{
    public class FeatureCreate
    {
        public GameType GameType { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
        public string Special { get; set; }
        public int? RacePointCost { get; set; }
        public List<int> RaceIdPrerequisite { get; set; }
        public List<int> ClassIdPrerequisite { get; set; }
        public List<int> FeatureIdPrerequisite { get; set; }
        public Dictionary<string, int> StatPrerequisite { get; set; }
        public int? LvlPrerequisite { get; set; }
        public double StrengthModifier { get; set; }
        public double DexterityModifier { get; set; }
        public double ConstitutionModifier { get; set; }
        public double IntelligenceModifier { get; set; }
        public double WisdomModifier { get; set; }
        public double CharismaModifier { get; set; }
    }
}
