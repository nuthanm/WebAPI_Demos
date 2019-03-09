using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
namespace WebAPI_Demos.Controllers
{
    public class EmployeesController : ApiController
    {
        /// <summary>
        /// Get All employee Details
        /// </summary>
        /// <returns>Full List of Employee Details</returns>
        [HttpGet]
        public HttpResponseMessage LoadAllEmployees(string gender = "All")
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
               entities.Employees.Where(entity => entity.Gender == "male").ToList());

                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
           entities.Employees.Where(entity => entity.Gender == "female").ToList());

                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value must be either All,Male or Female only");


                }

            }
        }

        /// <summary>
        /// Get specific employee Details
        /// </summary>
        /// <returns>Get selected employee details</returns>
        [HttpGet]
        public HttpResponseMessage LoadSelectedEmployee(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(employee => employee.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Employee with this id: {0} was not found", id));
                }
            }
        }

        /// <summary>
        /// Create a new employee details
        /// If we add return type is void then status code is 204 : No content
        /// In General, If we create any new record then status code is 201 : Item Created and send the response with 
        /// employee object as per this example
        /// </summary>
        /// <param name="employee"></param>
        public HttpResponseMessage Post(Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                }

                var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Delete the selected employee tables and it returns 204 because return type is void
        /// 
        /// </summary>
        /// <param name="id"></param>

        public HttpResponseMessage put(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entitiy = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entitiy != null)

                    {
                        entitiy.FirstName = employee.FirstName;
                        entitiy.LastName = employee.LastName;
                        entitiy.Gender = employee.Gender;
                        entitiy.Salary = employee.Salary;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Employee with this Id {0}  was not found", id));
                    }

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
                    var entity = entities.Employees.FirstOrDefault(employee => employee.ID == id);
                    if (entity != null)
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + "was not found");
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
