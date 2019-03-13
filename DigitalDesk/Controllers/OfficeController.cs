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
        public IEnumerable<Employee> GetEmployeeList()
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

        public HttpResponseMessage GetEmployee(string id)
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

        //public HttpResponseMessage PostEmployee([FromBody]Employee employee)
        //{
        //    try
        //    {
        //        using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
        //        {
        //            dbfinal.Employees.Add(employee);
        //            dbfinal.SaveChanges();

        //            var response = Request.CreateResponse(HttpStatusCode.Created, employee);
        //            response.Headers.Location = new Uri(Request.RequestUri + employee.EmpId.ToString());
        //            return response;
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, x);
        //    }
        //}

        public HttpResponseMessage DeleteEmployee(string id)
        {
            try
            {
                using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
                {
                    var response = dbfinal.Employees.FirstOrDefault(e => e.EmpId == id);
                    if (response == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id: " + id.ToString() + "not found");
                    }
                    else
                    {
                        dbfinal.Employees.Remove(response);
                        dbfinal.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        public HttpResponseMessage Put(string id,[FromBody]Employee employee)
        {
            try
            {
                using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
                {
                    var response = dbfinal.Employees.FirstOrDefault(e => e.EmpId == id);
                    if (response == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id :" + id.ToString() + "does not exist");
                    }
                    else
                    {
                        response.EmpName = employee.EmpName;
                        response.Email = employee.Email;
                        response.Address = response.Address;
                        response.City = employee.City;
                        response.Country = employee.Country;
                        response.State = employee.State;
                        response.PhoneNo = employee.PhoneNo;
                        response.Designation = employee.Designation;
                        response.Department = employee.Department;

                        dbfinal.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //APIs for Office Controller
        
        public HttpResponseMessage Getofficelist()
        {
            try
            {
                using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
                {
                    var offlist = (from o in dbfinal.Offices select new { o.OfficeId, o.OfficeName}).ToList();
                    if (offlist != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, offlist);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Get out");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        
        public HttpResponseMessage Getfloorlist(int id)
        {
            try
            {
                using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
                {
                    var floorlist = (from e in dbfinal.FloorInfoes
                                     join f in dbfinal.Offices on e.OfficeId equals f.OfficeId
                                     where e.OfficeId == id
                                     select e.FloorNo).ToList();
                    if (floorlist != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, floorlist);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Get out");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //APIs for Info controller
        //Chart data 
        public HttpResponseMessage Getseats(int offid, int floorid)
        {
            try
            {
                using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
                {

                    var totalseats = (from f in dbfinal.FloorInfoes
                                      join o in dbfinal.Offices on f.OfficeId equals o.OfficeId
                                      where o.OfficeId == offid && f.FloorId == floorid
                                      select f.NoOfSeats).FirstOrDefault();

                    var allocatedseats = (from f in dbfinal.FloorInfoes
                                          join s in dbfinal.SeatDetails on f.FloorId equals s.FloorId
                                          join o in dbfinal.Offices on s.OfficeId equals o.OfficeId
                                          where s.OfficeId == offid && s.FloorId == floorid && s.IsAllocated == true
                                          select s.IsAllocated).Count();
                    var offname = (from o in dbfinal.Offices where o.OfficeId == 1 select o.OfficeName).FirstOrDefault();

                   var availableseats = totalseats - allocatedseats;
                    if (totalseats > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { totalseats, allocatedseats, availableseats, offname });
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Get out");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Director's data

        //public HttpResponseMessage GetDirectorData()
        //{
        //    try
        //    {
        //        using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities)
        //        {
        //            var dirdata = dbfinal.Cities.ToList();
        //            return requedirdata;
        //        }
        //    }
        //    catch(Exception)
        //    {

        //    }
        //}
    }
}