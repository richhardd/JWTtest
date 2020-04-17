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
    [Authorize]
    public class LoginController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
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

        [HttpGet]
        public HttpResponseMessage Validate (string token, string username)
        {
            bool exists = new UserRepository().GetUser(username) != null;
            if (!exists) 
                return Request.CreateResponse(HttpStatusCode.NotFound, "The user was not found.");
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}
