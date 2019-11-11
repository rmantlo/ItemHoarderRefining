using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Items
{
    public class InventoryItems
    {
        [Key]
        public int ItemID { get; set; }
        public Guid OwnerID { get; set; }
        [ForeignKey("OriginalItem")]
        public int OriginalItemID { get; set; }
        public virtual Item OriginalItem { get; set; }
        //pick random num between array nums [fragile value, resilient value]
        public int ActualHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public bool IsEquipted { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
