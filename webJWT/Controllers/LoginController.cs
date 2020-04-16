using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webJWT.Repositories;
using webJWT.Models;

namespace webJWT.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login (User user)
        {
            User u = new UserRepository().GetUser(user.username);
            if (u == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "The user was not found.");
            bool credentials = u.password.Equals(user.password);
            if (!credentials)
                return Request.CreateResponse(HttpStatusCode.Forbidden, "The username/password combination was wrong.");
            return Request.CreateResponse(HttpStatusCode.OK,TokenManager.GenerateToken(user.username));
        }
    }
}
