using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.MultiTenancy.TenantProviders
{
    public class TenantProvider : ITenantProvider
    {
        private Guid _tenantId;

        public TenantProvider(IHttpContextAccessor accessor, TenantsContext context)
        {
            var host = accessor.HttpContext.Request.Host.Value;
            _tenantId = context.GetTenantId(host);
        }

        public Guid GetTenantId()
        {
            return _tenantId;
        }
    }
}
