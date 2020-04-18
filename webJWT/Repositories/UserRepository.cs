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
            TestUsers.Add(new User() { username = "test1", password = "pass1" , role = UserRole.ADMIN});
            TestUsers.Add(new User() { username = "test2", password = "pass2" , role = UserRole.NORMAL});
            TestUsers.Add(new User(){username = "test3", password = "pass3",role=UserRole.NORMAL})
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