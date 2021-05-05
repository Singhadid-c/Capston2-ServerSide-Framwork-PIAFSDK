using Backend_PI.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend_PI.Controllers
{
    public class AuthController : ApiController
    {
        // http://localhost:xxx/api/auth/user
        [HttpGet]
        [ActionName("user")]
        public IHttpActionResult GetUsers()
        {
            using (Capstone2Entities _databaseContext = new Capstone2Entities())
            {
                var users = _databaseContext.Users.ToList();
                return Ok(new { result = users, message = "success" });
            }
        }

        [HttpGet]
        [ActionName("user")]
        public IHttpActionResult GetUsersByid(int id)
        {
            using (Capstone2Entities _databaseContext = new Capstone2Entities())
            {
                var user = _databaseContext.Users.SingleOrDefault(o => o.id == id);
                return Ok(new { result = user, message = "success" });
            }
        }
    }
}