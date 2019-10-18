using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characteristics.Features
{
    public class FeatureIndex
    {
        public int FeatureID { get; set; }
        public Guid OwnerID { get; set; }
        public GameType GameType { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public int? RacePointCost { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
