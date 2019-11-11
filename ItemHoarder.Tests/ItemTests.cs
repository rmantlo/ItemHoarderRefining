using ItemHoarder.Data.Items;
using ItemHoarder.Models.Items;
using ItemHoarder.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Tests
{
    [TestClass]
    public class ItemTests
    {
        public List<Item> _list;
        [TestInitialize]
        public void Arrange()
        {
            _list = new List<Item>();
            var item = new Other
            {

            };
            var armor = new Armor
            {

            };
            var weapon = new Weapon
            {

            };
            _list.Add(item);
            _list.Add(armor);
            _list.Add(weapon);
        }
        [TestMethod]
        public void ItemGet()
        {
            var pls = _list[0].GetType();
            var get = _list.Select(e => new ItemIndex
            {
                ItemID = e.ItemID,
                OwnerID = e.OwnerID,
                ItemName = e.ItemName,
                Description = e.Description,
                ItemType = e.GetType(),
                ItemPhoto = e.ItemPhoto.ToArray()[0],
                CostPlatinum = e.CostPlatinum,
                CostGold = e.CostGold,
                CostElectrum = e.CostElectrum,
                CostSilver = e.CostSilver,
                CostCopper = e.CostCopper,
                ItemRarity = e.ItemRarity,
                DateOfCreation = e.DateOfCreation,
                DateOfModification = e.DateOfModification
            }).ToList();
        }
        [TestMethod]
        public void ItemCreate()
        {
            var service = new ItemService(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            var item = new OtherItemCreate();
            item.ItemClass = Data.ItemClass.ArtisanTools;
            var armor = new ArmorCreate();
            var weapon = new WeaponCreate();


            var result = service.CreateItem(item);
        }
    }
}
