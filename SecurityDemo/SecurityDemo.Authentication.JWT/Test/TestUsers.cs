using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDemo.Authentication.JWT
{
    public static class TestUsers
    {
        public static List<User> Users = new List<User>
        {
            new User{ Id = Guid.NewGuid(), UserName = "Paul", Password = "Paul123", Roles = new List<string>{ "administrator", "api_access" }, Urls = new List<string>{ "/api/values/getadminvalue", "/api/values/getguestvalue" }},
            new User{ Id = Guid.NewGuid(), UserName = "Young", Password = "Young123", Roles = new List<string>{ "api_access" }, Urls = new List<string>{ "/api/values/getguestvalue" }},
            new User{ Id = Guid.NewGuid(), UserName = "Roy", Password = "Roy123", Roles = new List<string>{ "administrator" }, Urls = new List<string>{ "/api/values/getadminvalue" }},
        };
    }

    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Urls { get; set; }
    }
}
