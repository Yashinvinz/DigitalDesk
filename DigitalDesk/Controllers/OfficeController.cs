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

        public HttpResponseMessage Get(string id)
        {
            using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
            {
                var entity = dbfinal.Employees.FirstOrDefault(e => e.EmpId == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with id : " + id.ToString() + " does not exist");
                }
            }
        }

        public HttpResponseMessage Post(Employee employee)
        {
            try
            {
                using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
                {
                    dbfinal.Employees.Add(employee);
                    dbfinal.SaveChanges();

                    var response = Request.CreateResponse(HttpStatusCode.Created, employee);
                    response.Headers.Location = new Uri(Request.RequestUri + employee.EmpId.ToString());
                    return response;
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, x);
            }
        }

        
    }
}