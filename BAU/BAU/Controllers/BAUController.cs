using BAU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAU.Controllers
{
    public class BAUController : Controller
    {
        // GET: BAU
        HelpingClass hlp = new HelpingClass();//Helping class object
        dbBAUEntities context = new dbBAUEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Rotation values)
        {
            if (ModelState.IsValid)
            {
                int RotationPeriod = 14;
                int result = hlp.runRotationMethords(values, RotationPeriod);
                if (result == 1)
                {
                    return RedirectToAction("RotaionPeriod");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult RotaionPeriod()
        {
            ViewBag.msg = "Successfully Added New Rotation Date";
            var i = context.Rotations.ToList();
            return View(i);
        }
    }
}