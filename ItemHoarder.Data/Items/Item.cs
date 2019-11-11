using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Items
{
    public abstract class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string Description { get; set; }
        public ICollection<Photo> ItemPhoto { get; set; }
        public float Weight { get; set; }
        public int? CostPlatinum { get; set; }
        public int? CostGold { get; set; }
        public int? CostSilver { get; set; }
        public int? CostElectrum { get; set; }
        public int? CostCopper { get; set; }
        //sending in array as string [fragile value, resilient value]
        public string HitPoints { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
    public class Other : Item
    {
        public ItemClass ItemClass { get; set; }
    }
    public class Weapon : Item
    {
        public WeaponType WeaponType { get; set; }
        public Range RangeType { get; set; }
        public int RangeNormal { get; set; }
        public int RangeLong { get; set; }
        public int BaseArmorClass { get; set; }
        public int DamageDice { get; set; }
        public int NumberOfDice { get; set; }
        public DamageType DamageType { get; set; }
        public double DamageResiliance { get; set; }
        public string SpecialEffects { get; set; }
        //weapons contain properties (like features...)
    }
    public class Armor : Item
    {
        public ArmorType ArmorType { get; set; }
        public bool StealthDisadvantage { get; set; }
        public double DamageResiliance { get; set; }
        public int BaseArmorClass { get; set; }
        public int StrMinimum { get; set; }
        public string SpecialEffects { get; set; }

    }
}
