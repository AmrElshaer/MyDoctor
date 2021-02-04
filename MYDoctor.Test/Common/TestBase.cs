using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Test.Common
{
    public class TestBase : IDisposable
    {
        protected ApplicationDbContext Context { get; set; }
        public TestBase()
        {
            Context = TestContextFactory.Create();
        }
        public void Dispose()
        {
            TestContextFactory.Destroy(Context);
        }
    }
}
