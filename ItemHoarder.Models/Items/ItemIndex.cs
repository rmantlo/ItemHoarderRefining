using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Items
{
    public class ItemIndex
    {
        public int ItemID { get; set; }
        public Guid OwnerID { get; set; }
        public Photo ItemPhoto { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public Type ItemType { get; set; }
        public int? CostPlatinum { get; set; }
        public int? CostGold { get; set; }
        public int? CostSilver { get; set; }
        public int? CostElectrum { get; set; }
        public int? CostCopper { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
