using System;
using Microsoft.Extensions.Configuration;
using netcore_api_testing;

namespace test
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        { }
    }
}
