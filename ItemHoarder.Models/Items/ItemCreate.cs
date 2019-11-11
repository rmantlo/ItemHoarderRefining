using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ItemHoarder.Models.Items
{
    public abstract class ItemCreate
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase PhotoUpload { get; set; }
        public float Weight { get; set; }
        public int? CostPlatinum { get; set; }
        public int? CostGold { get; set; }
        public int? CostSilver { get; set; }
        public int? CostElectrum { get; set; }
        public int? CostCopper { get; set; }
        public List<int> HitPoints { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

    }
    public class OtherItemCreate : ItemCreate
    {
        public ItemClass ItemClass { get; set; }
    }
    public class WeaponCreate : ItemCreate
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
    public class ArmorCreate: ItemCreate
    {
        public ArmorType ArmorType { get; set; }
        public bool StealthDisadvantage { get; set; }
        public double DamageResiliance { get; set; }
        public int BaseArmorClass { get; set; }
        public int StrMinimum { get; set; }
    }
}
