using ItemHoarder.Models.Profile;
using ItemHoarder.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemHoarder.MVC.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var service = CreateProfileService();
            var profile = service.GetMyProfile();
            return View(profile);
        }

        //update created profile
        public ActionResult Edit()
        {
            var service = CreateProfileService();
            var info = service.GetMyProfile();
            return View(new ProfileUpdate
            {
                UserID = info.UserID,
                About = info.About
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileUpdate update)
        {
            if (!ModelState.IsValid) return View(update);
            if(update.UserID != Guid.Parse(User.Identity.GetUserId()))
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(update);
            }
            var service = CreateProfileService();
            if (service.UpdateProfileInfo(update))
            {
                TempData["SaveResult"] = "Your Profile was updated";
                return RedirectToAction("Index", "Home");
            }
            return View(update);
        }

        private ProfileService CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ProfileService(userId);
        }
    }
}