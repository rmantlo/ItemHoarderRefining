using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Items
{
    public abstract class ItemDetails
    {
        public int ItemID { get; set; }
        public Guid OwnerID { get; set; }
        public Photo ItemPhoto { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int? CostPlatinum { get; set; }
        public int? CostGold { get; set; }
        public int? CostSilver { get; set; }
        public int? CostElectrum { get; set; }
        public int? CostCopper { get; set; }
        public List<int> HitPoints { get; set; }
        public float Weight { get; set; }
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
    public class WeaponDetails : ItemDetails
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
    }
    public class ArmorDetails : ItemDetails
    {
        public ArmorType ArmorType { get; set; }
        public bool StealthDisadvantage { get; set; }
        public double DamageResiliance { get; set; }
        public int BaseArmorClass { get; set; }
        public int StrMinimum { get; set; }
        public string SpecialEffects { get; set; }
    }
    public class OtherItemDetails : ItemDetails
    {
        public ItemClass ItemClass { get; set; }
    }

}
