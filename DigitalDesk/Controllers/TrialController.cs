using System.Collections.Generic;
using System.Web.Mvc;
using DigitalDesk.Models;
namespace DigitalDesk.Controllers
{
    public class TrialController : Controller
    {
        Trial trial = new Trial();
        // GET: Trial
        public ActionResult Trial()
        {
            List<string> emp = new List<string>();
            emp.Add("o");
            emp.Add("m");
            emp.Add("g");

            ViewBag.query = emp;

            return View();
        }
    }
}