using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecurityDemo.Authentication.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("[action]")]
        [Authorize(Policy = "APIAccess")]
        public ActionResult<IEnumerable<string>> GetValueByGuestPolicy()
        {
            return new string[] { "use policy = APIAccess" };
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "Administrator")]
        public ActionResult<IEnumerable<string>> GetValueByAdminPolicy()
        {
            return new string[] { "use policy = Administrator" };
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "administrator")]
        public ActionResult<IEnumerable<string>> GetValueByAdminRole()
        {
            return new string[] { "use Roles = administrator" };
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "Permission")]
        public ActionResult<IEnumerable<string>> GetAdminValue()
        {
            return new string[] { "use Policy = Permission" };
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "Permission")]
        public ActionResult<IEnumerable<string>> GetGuestValue()
        {
            return new string[] { "use Policy = Permission" };
        }
    }
}
