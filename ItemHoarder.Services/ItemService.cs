using ItemHoarder.Data;
using ItemHoarder.Data.Items;
using ItemHoarder.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Services
{
    public class ItemService
    {
        private readonly Guid _userId;
        public ItemService(Guid userId)
        {
            _userId = userId;
        }

        //get all
        public IEnumerable<ItemIndex> GetAllItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var items = ctx.Items.Include("ItemPhoto").Where(e => e.OwnerID == _userId).ToList();
                List<ItemIndex> allItems = new List<ItemIndex>();
                foreach (var item in items)
                {
                    var ok = item.GetType();
                    var it = new ItemIndex
                    {
                        ItemID = item.ItemID,
                        OwnerID = item.OwnerID,
                        ItemName = item.ItemName,
                        Description = item.Description,
                        ItemType = item.GetType(),
                        ItemPhoto = item.ItemPhoto.FirstOrDefault(),
                        CostPlatinum = item.CostPlatinum,
                        CostGold = item.CostGold,
                        CostElectrum = item.CostElectrum,
                        CostSilver = item.CostSilver,
                        CostCopper = item.CostCopper,
                        ItemRarity = item.ItemRarity,
                        DateOfCreation = item.DateOfCreation,
                        DateOfModification = item.DateOfModification
                    };
                    allItems.Add(it);
                }
                return allItems;
            }
        }
        //get one
        public ItemDetails GetItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var pls = ctx.Items.Include("ItemPhoto").Where(e => e.ItemID == id && e.OwnerID == _userId).ToList();
                if (pls[0].GetType() == typeof(Weapon))
                {
                    return GetWeaponDetails(pls.OfType<Weapon>().SingleOrDefault());
                }
                else if (pls[0].GetType() == typeof(Armor))
                {
                    return GetArmorDetails(pls.OfType<Armor>().SingleOrDefault());
                }
                else if (pls[0].GetType() == typeof(Other))
                {
                    return GetOtherItemDetails(pls.OfType<Other>().SingleOrDefault());
                }
                else return null;
            }
        }
        //create
        public bool CreateItem(ItemCreate newItem)
        {
            Item item = null;

            if (newItem.GetType() == typeof(OtherItemCreate))
            {
                var i = (OtherItemCreate)newItem;
                item = new Other
                {
                    ItemClass = i.ItemClass,
                    OwnerID = _userId,
                    ItemName = i.ItemName,
                    Description = i.Description,
                    Weight = i.Weight,
                    CostCopper = i.CostCopper,
                    CostElectrum = i.CostElectrum,
                    CostGold = i.CostGold,
                    CostPlatinum = i.CostPlatinum,
                    CostSilver = i.CostSilver,
                    HitPoints = String.Join("|", i.HitPoints),
                    ItemRarity = i.ItemRarity,
                    Strength = i.Strength,
                    Dexterity = i.Dexterity,
                    Constitution = i.Constitution,
                    Intelligence = i.Intelligence,
                    Wisdom = i.Wisdom,
                    Charisma = i.Charisma,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
            }
            else if (newItem.GetType() == typeof(WeaponCreate))
            {
                var i = (WeaponCreate)newItem;
                item = new Weapon
                {
                    OwnerID = _userId,
                    ItemName = i.ItemName,
                    Description = i.Description,
                    Weight = i.Weight,
                    CostCopper = i.CostCopper,
                    CostElectrum = i.CostElectrum,
                    CostGold = i.CostGold,
                    CostPlatinum = i.CostPlatinum,
                    CostSilver = i.CostSilver,
                    HitPoints = String.Join("|", i.HitPoints),
                    ItemRarity = i.ItemRarity,
                    Strength = i.Strength,
                    Dexterity = i.Dexterity,
                    Constitution = i.Constitution,
                    Intelligence = i.Intelligence,
                    Wisdom = i.Wisdom,
                    Charisma = i.Charisma,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    WeaponType = i.WeaponType,
                    RangeType = i.RangeType,
                    RangeNormal = i.RangeNormal,
                    RangeLong = i.RangeLong,
                    BaseArmorClass = i.BaseArmorClass,
                    DamageDice = i.DamageDice,
                    NumberOfDice = i.NumberOfDice,
                    DamageType = i.DamageType,
                    DamageResiliance = i.DamageResiliance,
                    SpecialEffects = i.SpecialEffects
                };
            }
            else if (newItem.GetType() == typeof(ArmorCreate))
            {
                var i = (ArmorCreate)newItem;
                item = new Armor
                {
                    OwnerID = _userId,
                    ItemName = i.ItemName,
                    Description = i.Description,
                    Weight = i.Weight,
                    CostCopper = i.CostCopper,
                    CostElectrum = i.CostElectrum,
                    CostGold = i.CostGold,
                    CostPlatinum = i.CostPlatinum,
                    CostSilver = i.CostSilver,
                    HitPoints = String.Join("|", i.HitPoints),
                    ItemRarity = i.ItemRarity,
                    Strength = i.Strength,
                    Dexterity = i.Dexterity,
                    Constitution = i.Constitution,
                    Intelligence = i.Intelligence,
                    Wisdom = i.Wisdom,
                    Charisma = i.Charisma,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    ArmorType = i.ArmorType,
                    StealthDisadvantage = i.StealthDisadvantage,
                    DamageResiliance = i.DamageResiliance,
                    BaseArmorClass = i.BaseArmorClass,
                    StrMinimum = i.StrMinimum,
                };
            }
            if (newItem.PhotoUpload != null && newItem.PhotoUpload.ContentLength > 0)
            {
                var photo = new Photo
                {
                    PhotoName = System.IO.Path.GetFileName(newItem.PhotoUpload.FileName),
                    FileType = FileType.Item,
                    ContentType = newItem.PhotoUpload.ContentType,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                using (var reader = new System.IO.BinaryReader(newItem.PhotoUpload.InputStream))
                {
                    photo.Content = reader.ReadBytes(newItem.PhotoUpload.ContentLength);
                }
                item.ItemPhoto = new List<Photo> { photo };
            }
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(item);
                var save = ctx.SaveChanges();
                if (save == 1 || save == 3)
                {
                    return true;
                }
                else return false;
            }
        }
        //edit
        //public bool EditItem(ItemEdit item)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        if (item.GetType() == typeof(WeaponEdit))
        //        {
        //            var oldItem = ctx.Items.Include("ItemPhoto").OfType<Weapon>().SingleOrDefault(e => e.OwnerID == _userId && e.ItemID == item.ItemID);
        //            if (item.ItemUpload != null)
        //            {
        //                oldItem.ItemPhoto.Clear();
        //                var photo = new Photo
        //                {
        //                    PhotoName = System.IO.Path.GetFileName(item.ItemUpload.FileName),
        //                    FileType = FileType.Profile,
        //                    ContentType = item.ItemUpload.ContentType,
        //                    DateOfCreation = DateTimeOffset.UtcNow
        //                };
        //                using (var reader = new System.IO.BinaryReader(item.ItemUpload.InputStream))
        //                {
        //                    photo.Content = reader.ReadBytes(item.ItemUpload.ContentLength);
        //                }
        //                if (oldItem.ItemPhoto != null)
        //                {
        //                    oldItem.ItemPhoto.Clear();
        //                }
        //                oldItem.ItemPhoto = new List<Photo> { photo };
        //            }
        //            oldItem.ItemName = item.ItemName;
        //            oldItem.Description = item.Description;
        //            oldItem.CostCopper = item.CostCopper;
        //            oldItem.CostElectrum = item.CostElectrum;
        //            oldItem.CostGold = item.CostGold;
        //            oldItem.CostPlatinum = item.CostPlatinum;
        //            oldItem.CostSilver = item.CostSilver;
        //            oldItem.HitPoints = String.Join("|", item.HitPoints);
        //            oldItem.ItemRarity = item.ItemRarity;
        //            oldItem.Strength = item.Strength;
        //            oldItem.Dexterity = item.Dexterity;
        //            oldItem.Constitution = item.Constitution;
        //            oldItem.Intelligence = item.Intelligence;
        //            oldItem.Wisdom = item.Wisdom;
        //            oldItem.Charisma = item.Charisma;
        //            oldItem.DateOfModification = DateTimeOffset.Now;
        //        }
        //        else if (item.GetType() == typeof(Armor))
        //        {
        //            var oldItem = ctx.Items.Include("ItemPhoto").OfType<Weapon>().SingleOrDefault(e => e.OwnerID == _userId && e.ItemID == item.ItemID);
        //        }
        //        else if (item.GetType() == typeof(Other))
        //        {
        //            var oldItem = ctx.Items.Include("ItemPhoto").OfType<Weapon>().SingleOrDefault(e => e.OwnerID == _userId && e.ItemID == item.ItemID);
        //        }
        //        else return false;

        //    }
        //}
        ////delete
        public bool DeleteItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.Single(e => e.OwnerID == _userId && e.ItemID == id);
                ctx.Items.Remove(item);
                return ctx.SaveChanges() == 2;
            }
        }

        private WeaponDetails GetWeaponDetails(Weapon item)
        {
            return new WeaponDetails
            {
                ItemID = item.ItemID,
                OwnerID = item.OwnerID,
                ItemPhoto = item.ItemPhoto.Count > 0 ? item.ItemPhoto.FirstOrDefault() : null,
                ItemName = item.ItemName,
                Weight = item.Weight,
                Description = item.Description,
                CostCopper = item.CostCopper,
                CostElectrum = item.CostElectrum,
                CostGold = item.CostGold,
                CostPlatinum = item.CostPlatinum,
                CostSilver = item.CostSilver,
                ItemRarity = item.ItemRarity,
                HitPoints = new List<int> { int.Parse(item.HitPoints.Split('|')[0]), int.Parse(item.HitPoints.Split('|')[1]) },
                Strength = item.Strength,
                Dexterity = item.Dexterity,
                Constitution = item.Constitution,
                Intelligence = item.Intelligence,
                Wisdom = item.Wisdom,
                Charisma = item.Charisma,
                DateOfCreation = item.DateOfCreation,
                DateOfModification = item.DateOfModification,
                WeaponType = item.WeaponType,
                RangeType = item.RangeType,
                RangeLong = item.RangeLong,
                RangeNormal = item.RangeNormal,
                BaseArmorClass = item.BaseArmorClass,
                DamageDice = item.DamageDice,
                NumberOfDice = item.NumberOfDice,
                DamageType = item.DamageType,
                DamageResiliance = item.DamageResiliance,
                SpecialEffects = item.SpecialEffects
            };
        }
        private ArmorDetails GetArmorDetails(Armor item)
        {
            return new ArmorDetails
            {
                ItemID = item.ItemID,
                OwnerID = item.OwnerID,
                ItemPhoto = item.ItemPhoto.Count > 0 ? item.ItemPhoto.FirstOrDefault() : null,
                ItemName = item.ItemName,
                Description = item.Description,
                Weight = item.Weight,
                CostCopper = item.CostCopper,
                CostElectrum = item.CostElectrum,
                CostGold = item.CostGold,
                CostPlatinum = item.CostPlatinum,
                CostSilver = item.CostSilver,
                ItemRarity = item.ItemRarity,
                HitPoints = new List<int> { int.Parse(item.HitPoints.Split('|')[0]), int.Parse(item.HitPoints.Split('|')[1]) },
                Strength = item.Strength,
                Dexterity = item.Dexterity,
                Constitution = item.Constitution,
                Intelligence = item.Intelligence,
                Wisdom = item.Wisdom,
                Charisma = item.Charisma,
                DateOfCreation = item.DateOfCreation,
                DateOfModification = item.DateOfModification,
                ArmorType = item.ArmorType,
                StealthDisadvantage = item.StealthDisadvantage,
                DamageResiliance = item.DamageResiliance,
                BaseArmorClass = item.BaseArmorClass,
                StrMinimum = item.StrMinimum,
                SpecialEffects = item.SpecialEffects
            };
        }
        private OtherItemDetails GetOtherItemDetails(Other item)
        {
            return new OtherItemDetails
            {
                ItemID = item.ItemID,
                OwnerID = item.OwnerID,
                ItemPhoto = item.ItemPhoto.Count > 0 ? item.ItemPhoto.FirstOrDefault() : null,
                ItemName = item.ItemName,
                Description = item.Description,
                Weight = item.Weight,
                CostCopper = item.CostCopper,
                CostElectrum = item.CostElectrum,
                CostGold = item.CostGold,
                CostPlatinum = item.CostPlatinum,
                CostSilver = item.CostSilver,
                ItemRarity = item.ItemRarity,
                HitPoints = new List<int> { int.Parse(item.HitPoints.Split('|')[0]), int.Parse(item.HitPoints.Split('|')[1]) },
                Strength = item.Strength,
                Dexterity = item.Dexterity,
                Constitution = item.Constitution,
                Intelligence = item.Intelligence,
                Wisdom = item.Wisdom,
                Charisma = item.Charisma,
                DateOfCreation = item.DateOfCreation,
                DateOfModification = item.DateOfModification,
                ItemClass = item.ItemClass
            };
        }
    }
}
