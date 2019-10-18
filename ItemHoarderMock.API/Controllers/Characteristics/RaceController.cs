using ItemHoarder.Services.Characteristics;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ItemHoarderMock.API.Controllers.Characteristics
{
    public class RaceController : ApiController
    {
        // GET: Race
        public IHttpActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            //return new RaceService(userId);
            RaceService service = new RaceService(userId);
            return Ok(service.GetAllMyRaces());
        }
    }
}