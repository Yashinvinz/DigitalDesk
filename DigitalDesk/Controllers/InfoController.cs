using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeDataAccess;
using Officedatas;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace DigitalDesk.Controllers
{
    public class InfoController : ApiController
    {
        // GET: Info
        public HttpResponseMessage Getdirdata(int offid, int floorid)
        {
            try
            {
                using (DigitalDeskManagementFinalDBEntities db = new DigitalDeskManagementFinalDBEntities())
                {
                    var result = (from e in db.Employees
                                  join m in db.ManagerCabins on e.EmpId equals m.ManagerId
                                  join d in db.Departments on e.Department equals d.DeptId
                                  where m.OfficeId == offid && m.FloorId == floorid && e.Designation == 2
                                  select new { e.EmpId, e.EmpName, d.DeptName }).ToList();
                    if (result != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Content not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}