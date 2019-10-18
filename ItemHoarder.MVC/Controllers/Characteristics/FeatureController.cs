using ItemHoarder.Models.Characteristics.Features;
using ItemHoarder.Services.Characteristics;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemHoarder.MVC.Controllers.Characteristics
{
    public class FeatureController : Controller
    {
        // GET: Feature
        public ActionResult Index()
        {
            var service = CreateFeatService();
            return View(service.GetAllFeatures());
        }
        //details
        public ActionResult Details(int id)
        {
            var service = CreateFeatService();
            
            return View(service.GetFeatureById(id));
        }
        //create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeatureCreate feat)
        {
            if (!ModelState.IsValid) return View(feat);
            var service = CreateFeatService();
            if (service.CreateFeature(feat))
            {
                TempData["SaveResult"] = "Feature Created";
                return RedirectToAction("Index");
            }
            return View(feat);
        }
        //edit, only main feat info
        public ActionResult Edit(int featId)
        {
            var service = CreateFeatService();
            return View(service.GetFeatureById(featId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FeatureDetails feat)
        {
            if (!ModelState.IsValid) return View(feat);
            var service = CreateFeatService();
            if (service.UpdateFeature(feat))
            {
                TempData["SaveResult"] = "Feature Updated";
                return RedirectToAction("Index");
            }
            return View(feat);
        }
        

        //edit for race/feats/class prerequisites connected to a feature
        //
        //
        //
        //

        //delete
        public ActionResult Delete(int id)
        {
            var service = CreateFeatService();
            return View(service.GetFeatureById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFeat(int id)
        {
            var service = CreateFeatService();
            if (service.DeleteFeature(id))
            {
                TempData["SaveResult"] = "Feature Deleted";
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Delete", new { id = id });
        }

        private FeatureService CreateFeatService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new FeatureService(userId);
        }
    }
}