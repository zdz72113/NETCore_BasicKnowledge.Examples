using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDemo
{
    public interface ITest
    {
        Guid Guid { get; }
    }

    public class Test : ITest
    {
        public Guid Guid { get; }

        public Test()
        {
            Guid = Guid.NewGuid();
        }
    }
}
