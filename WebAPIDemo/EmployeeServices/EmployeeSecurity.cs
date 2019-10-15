using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace EmployeeServices
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Usersses.Any(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password == password);

            }
        }
    }
}