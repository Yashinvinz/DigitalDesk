using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeDataAccess;


using System.Web.Http.Results;
using FinalData;

namespace DigitalDesk.Controllers
{
    public class OfficeController : ApiController
    {
        public DigitalDeskManagementFinalDBEntities Db = new DigitalDeskManagementFinalDBEntities();
        
        //Another method to send list as json

        //public IEnumerable<Employee> GetEmployeeList()
        //{
        //    using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
        //    {
        //        return dbfinal.Employees.ToList();
        //    }
        //}



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

        //public HttpResponseMessage Put(string id,[FromBody]Employee employee)
        //{
        //    try
        //    {
        //        using (SeatAllocationDBFinalEntities dbfinal = new SeatAllocationDBFinalEntities())
        //        {
        //            var response = dbfinal.Employees.FirstOrDefault(e => e.EmpId == id);
        //            if (response == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id :" + id.ToString() + "does not exist");
        //            }
        //            else
        //            {
        //                response.EmpName = employee.EmpName;
        //                response.Email = employee.Email;
        //                response.Address = response.Address;
        //                response.City = employee.City;
        //                response.Country = employee.Country;
        //                response.State = employee.State;
        //                response.PhoneNo = employee.PhoneNo;
        //                response.Designation = employee.Designation;
        //                response.Department = employee.Department;

        //                dbfinal.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, response);
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

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
        public HttpResponseMessage GetTotalSeats(int offid, int floorid)
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
                    var offname = (from o in dbfinal.Offices where o.OfficeId == offid select o.OfficeName).FirstOrDefault();

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

        public HttpResponseMessage GetDirector(int officeId, int floorId)
        {
            try
            {
                var dirdata = (from e in Db.Employees
                               join d in Db.Departments on e.Department equals d.DeptId
                               join m in Db.ManagerCabins on e.EmpId equals m.ManagerId
                               where m.OfficeId == officeId && m.FloorId == floorId
                               select new { e.EmpId, e.EmpName, d.DeptName}).ToList();
                if (dirdata != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, dirdata);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Get out");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Dir dashboard
        public HttpResponseMessage GetDirectorInfo(string managerId)
        {
            try
            {
                var dirinfo = (from e in Db.Employees
                               join d in Db.Departments on e.Department equals d.DeptId
                               join s in Db.Designations on e.Designation equals s.DesgId
                               where e.EmpId == managerId
                               select new { e.EmpName, d.DeptName, s.DesgName }).ToList();

                var seatcount = Db.ManagerToSeatPoolTemps.Where(e => e.ManagerId == managerId).Select(e => e.SeatId).ToList().Count();

                var allocatedcount = (from m in Db.ManagerToSeatPoolTemps
                                      join s in Db.SeatDetails on m.SeatId equals s.SeatId
                                      where s.IsAllocated == true && m.ManagerId == managerId
                                      select s.SeatId).ToList().Count();

                var unallocatedcount = seatcount - allocatedcount;

                if (dirinfo != null && seatcount > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { dirinfo, seatcount, allocatedcount, unallocatedcount});
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        public HttpResponseMessage GetDirectorPool(string managerId)
        {
            try
            {
                var allseats = (from m in Db.ManagerToSeatPoolTemps
                                join s in Db.SeatDetails on m.SeatId equals s.SeatId
                                where m.ManagerId == managerId
                                select s.SeatId).ToList();

                var allocatedseats = (from m in Db.ManagerToSeatPoolTemps
                                      join s in Db.SeatDetails on m.SeatId equals s.SeatId
                                      where s.IsAllocated == true && m.ManagerId == managerId
                                      select s.SeatId).ToList();

                if (allseats != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { allseats, allocatedseats });
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content found");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
       
    }
}