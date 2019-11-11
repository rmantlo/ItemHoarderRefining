using ItemHoarder.Models.Items;
using ItemHoarder.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemHoarder.MVC.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            var service = new ItemService(Guid.Parse(User.Identity.GetUserId()));
            var items = service.GetAllItems();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }
        //public ActionResult WeaponCreate()
        //{
        //    return PartialView("_WeaponCreate");
        //}
        //public ActionResult ArmorCreate()
        //{
        //    return PartialView("_ArmorCreate");
        //}
        //public ActionResult ItemCreate()
        //{
        //    return PartialView("_ItemCreate");
        //}

        private ActionResult Create(ItemCreate item)
        {

            if (!ModelState.IsValid) return View(item);
            var service = new ItemService(Guid.Parse(User.Identity.GetUserId()));
            if (service.CreateItem(item))
            {
                return RedirectToAction("Index", "Item");
            }
            else return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOtherItem(OtherItemCreate item) => Create(item);
        public ActionResult CreateWeaponItem(WeaponCreate item) => Create(item);
        public ActionResult CreateArmorItem(ArmorCreate item) => Create(item);

        //get by id
        public ActionResult Details(int id)
        {
            var service = new ItemService(Guid.Parse(User.Identity.GetUserId()));
            var some = service.GetItemById(id);
            return View(some);
        }
        //update by id
        public ActionResult Edit(int id)
        {
            var service = new ItemService(Guid.Parse(User.Identity.GetUserId()));
            var item = service.GetItemById(id);
            //var pls = ConvertItemToEdit(item);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private ActionResult Edit(ItemEdit item)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit", new { id = item.ItemID });
            return View();
        }
        public ActionResult EditOtherItem(OtherItemEdit item) => Edit(item);
        public ActionResult EditWeaponItem(WeaponEdit item) => Edit(item);
        public ActionResult EditArmorItem(ArmorEdit item) => Edit(item);
        //delete by id




        private ItemEdit ConvertItemToEdit(ItemDetails item)
        {
            if (item.GetType() == typeof(OtherItemDetails))
            {
                var other = (OtherItemDetails)item;
                return new OtherItemEdit
                {
                    ItemClass = other.ItemClass,
                    ItemName = other.ItemName,
                    Description = other.Description,
                    Weight = other.Weight,
                    CostCopper = other.CostCopper,
                    CostElectrum = other.CostElectrum,
                    CostGold = other.CostGold,
                    CostPlatinum = other.CostPlatinum,
                    CostSilver = other.CostSilver,
                    HitPoints = other.HitPoints,
                    ItemRarity = other.ItemRarity,
                    Strength = other.Strength,
                    Dexterity = other.Dexterity,
                    Constitution = other.Constitution,
                    Intelligence = other.Intelligence,
                    Wisdom = other.Wisdom,
                    Charisma = other.Charisma
                };
            }
            else if (item.GetType() == typeof(ArmorDetails))
            {
                var other = (ArmorDetails)item;
                return new ArmorEdit
                {
                    ItemName = other.ItemName,
                    Description = other.Description,
                    Weight = other.Weight,
                    CostCopper = other.CostCopper,
                    CostElectrum = other.CostElectrum,
                    CostGold = other.CostGold,
                    CostPlatinum = other.CostPlatinum,
                    CostSilver = other.CostSilver,
                    HitPoints = other.HitPoints,
                    ItemRarity = other.ItemRarity,
                    Strength = other.Strength,
                    Dexterity = other.Dexterity,
                    Constitution = other.Constitution,
                    Intelligence = other.Intelligence,
                    Wisdom = other.Wisdom,
                    Charisma = other.Charisma
                };
            }
            else if (item.GetType() == typeof(WeaponDetails))
            {
                var other = (WeaponDetails)item;
                return new WeaponEdit
                {
                    ItemName = other.ItemName,
                    Description = other.Description,
                    Weight = other.Weight,
                    CostCopper = other.CostCopper,
                    CostElectrum = other.CostElectrum,
                    CostGold = other.CostGold,
                    CostPlatinum = other.CostPlatinum,
                    CostSilver = other.CostSilver,
                    HitPoints = other.HitPoints,
                    ItemRarity = other.ItemRarity,
                    Strength = other.Strength,
                    Dexterity = other.Dexterity,
                    Constitution = other.Constitution,
                    Intelligence = other.Intelligence,
                    Wisdom = other.Wisdom,
                    Charisma = other.Charisma
                };
            }
            else return null;
        }
    }
}