using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BAU.Models;

namespace BAU.Controllers
{
    public class WebAPIEmployeeController : Controller
    {
        // GET: WebAPIEmployee
        public ActionResult Index()
        {
            //getting list of EMployees from WEb API
            IEnumerable<Employee> emplist;

            HttpResponseMessage response = WebApiAccessClass.apiclietn.GetAsync("Employees").Result;
            emplist = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            return View(emplist);
        }
    }
}