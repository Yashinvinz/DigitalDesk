using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeDataAccess;
using System.Web.Http.Results;

namespace DigitalDesk.Controllers
{
    public class OfficeController : ApiController
    {
       public IEnumerable<Employee> Get()
        {
            using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
            {
                return dbfinal.Employees.ToList();
            }
        }

        //Another method to send list as json

        //public JsonResult<Employee> Get(string id)
        //{
        //    using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
        //    {
        //        var employee = dbfinal.Employees.FirstOrDefault(e => e.EmpId == id);
        //        // return (Newtonsoft.Json.JsonConvert.SerializeObject(x));
        //        return Json(employee);

        //    }
        //}

        public Employee Get(string id)
        {
            using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
            {
                return dbfinal.Employees.FirstOrDefault(e => e.EmpId == id);
            }
        }
    }
}
