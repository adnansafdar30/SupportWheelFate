using BAU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAU.Controllers
{
    public class ShiftsController : Controller
    {
        HelpingClass hlp = new HelpingClass();
        //dbBAUEntities context = new dbBAUEntities();
        BAUTestEntities context = new BAUTestEntities();

        // GET: Shifts
        public ActionResult Index()
        {
            int result = hlp.getShifts();
            var i = context.Shifts.ToList();
            return View(i);
        }
    }
}