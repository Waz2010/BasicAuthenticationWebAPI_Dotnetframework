using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDataAccess;



namespace EmployeeServices.Controllers
{
    [EnableCorsAttribute("http://localhost:57042", "*", "*")]
    public class Employees1Controller : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (username.ToLower())
                {
                    //case "all":
                    //return Request.CreateResponse(HttpStatusCode.OK, entities.Employees1.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees1.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees1.Where(e => e.Gender.ToLower() == "female").ToList());
                     default:
                       return Request.CreateResponse(HttpStatusCode.BadRequest);
                        //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Query parameter " + gender + " is Invaled. Value for gender must be all, male or female ");
                        //return entity.Employees1.ToList();
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadAllEmployee(int id)
        {
            using (EmployeeDBEntities entity = new EmployeeDBEntities())
            {
                var message1 = entity.Employees1.FirstOrDefault(e => e.ID == id);

                if(message1 != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, message1);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "Not found.");
                }
            }
        }
        //to disable core [DisableCors]
        public HttpResponseMessage Post([FromBody] Employees1 employee)
        {
            try
            {
                using (EmployeeDBEntities entity = new EmployeeDBEntities())
                {
                    entity.Employees1.Add(employee);
                    entity.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees1.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not Found to delete.");
                    }
                    else
                    {
                        entities.Employees1.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Employees1 employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees1.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not Found to be update.");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
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
