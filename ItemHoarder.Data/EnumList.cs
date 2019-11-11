using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    //Proficiencies:
    public enum ProficiencyType
    {
        Armor, //Shields are armor type
        Weapons,
        ArtisanTools,
    }
    //items:
    public enum RarityOfItem
    {
        Common,
        Uncommon,
        Rare,
        VeryRare,
        Legendary
    }
    public enum ArmorType
    {
        Light,
        Medium,
        Heavy,
        Shield
    }
    public enum WeaponType
    {
        Simple,
        Martial,
    }
    public enum ItemClass
    {
        Potion,
        WonderousItem,
        ArtisanTools,
        Accessories,
        Etc
    }
    public enum Range
    {
        Melee,
        Ranged
    }
    public enum DamageType
    {
        Acid,
        Bludeoning,
        Cold,
        Fire,
        Force,
        Lightning,
        Necrotic,
        Piercing,
        Poison,
        Psychic,
        Radiant,
        Slashing,
        Thunder
    }


    public class EnumList
    {
    }
}
