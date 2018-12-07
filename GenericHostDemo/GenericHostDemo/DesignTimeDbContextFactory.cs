using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericHostDemo
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HostDemoContext>
    {
        public HostDemoContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HostDemoContext>();
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GenericHostDemo;Trusted_Connection=True;");
            return new HostDemoContext(builder.Options);
        }
    }
}
