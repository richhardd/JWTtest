using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webJWT
{
    public class User

    {
        public string username { set; get; }
        public string password { set; get; }

        public UserRole role { get; set; }
    }
}