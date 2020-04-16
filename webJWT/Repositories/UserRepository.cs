using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webJWT.Repositories
{
    public class UserRepository
    {
        public List<User> TestUsers;
        public UserRepository()
        {
            TestUsers = new List<User>();
            TestUsers.Add(new User() { username = "test1", password = "pass1" });
            TestUsers.Add(new User() { username = "test2", password = "pass2" });
        }
        public User GetUser(string username)
        {
            try
            {
                return TestUsers.First(u => u.username.Equals(username));
            }
            catch
            {
                return null;
            }
        }
    }
}