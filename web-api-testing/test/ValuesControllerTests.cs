using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using netcore_api_testing;

namespace test
{
    public class ValuesControllerTests
    {
        private static IWebHost App;

        // We use a static constructor here because we don't want to start up this test multiple times-
        // In XUnit, the test fixture class' instance constructor is called for each tests.
        // This thing sets up the api to run in the test project's process...
        // NOTE: if you are using MsTest instead of XUnit, you can use the `AssemblyInitializeAttribute` to do the same thing.
        static ValuesControllerTests()
        {
            App = WebHost.CreateDefaultBuilder()
                         .UseKestrel()
                         .UseContentRoot(Directory.GetCurrentDirectory())
                         // TODO: when i try to use TestStartup it fails...
                         .UseStartup<Startup>()
                         .UseUrls("http://localhost:9999")
                         .Build();

            App.Start();
        }

        [Fact]
        public async Task WhenGettingWithNoParams_ReturnsExpectedValues()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("http://localhost:9999/api/values");
                var resultBody = JsonConvert.DeserializeObject<string[]>(await result.Content.ReadAsStringAsync());

                resultBody
                      .Should()
                      .BeEquivalentTo(new string[]
                {
                    "value1",
                    "value2"
                });
            }
        }
    }
}
