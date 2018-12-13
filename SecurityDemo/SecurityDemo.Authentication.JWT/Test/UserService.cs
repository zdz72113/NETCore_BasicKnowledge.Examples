using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDemo.Authentication.JWT
{
    public interface IUserService
    {
        User GetUserByName(string name);
        List<string> GetFunctionsByUserId(Guid id);
    }

    public class UserService : IUserService
    {
        public List<string> GetFunctionsByUserId(Guid id)
        {
            var user = TestUsers.Users.SingleOrDefault(r => r.Id.Equals(id));
            return user?.Urls;
        }

        public User GetUserByName(string name)
        {
            var user = TestUsers.Users.SingleOrDefault(r => r.UserName.Equals(name));
            return user;
        }
    }
}
