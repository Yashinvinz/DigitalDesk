using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using DigitalDesk.Models;
namespace DigitalDesk.Controllers
{
    public class TrialController : Controller
    {
        
        // GET: Trial
        public ActionResult Trial()
        {
            IEnumerable<Trial> offlist = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64426/call/Office/Getofficelist/");

                var responseTask = client.GetAsync("office");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<IList<Trial>>();
                    readtask.Wait();


                    offlist = readtask.Result;
                }

                else
                {
                    offlist = Enumerable.Empty<Trial>();

                    ModelState.AddModelError(string.Empty, "error");
                }
            }

                return View(offlist);
        }
    }
}