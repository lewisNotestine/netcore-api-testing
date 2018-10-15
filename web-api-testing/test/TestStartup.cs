using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using netcore_api_testing.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace test
{
    /// <summary>
    /// This is the class that'll be used to start up the test API.
    /// If your API is simple enough, then you can just use the Startup.cs from the api project itself.
    /// However, having this separate will allow you to inject mocked services if you need them.
    /// </summary>
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // It is necessary to do more than just call services.AddMvc()
            // because we are actually serving up controllers defined in another project's assembly.
            // Obviously, we want to keep the test project separate from the project under test.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddApplicationPart(typeof(ValuesController).GetTypeInfo().Assembly)
                    .AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
