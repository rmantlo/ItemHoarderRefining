using ItemHoarder.Services.Characteristics;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemHoarderMock.API.Controllers.Characteristics
{
    public class RaceController : Controller
    {
        // GET: Race
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            RaceService service = new RaceService(userId);
            var race = service.GetAllMyRaces();
            return View(race);
        }
    }
}