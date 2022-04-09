using System.Collections.Generic;
using Confab.Bootstrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Confab.Shared.Tests
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly List<Configuration> _configurations = new();

        public TestApplicationFactory WithSetting(Configuration configuration)
        {
            _configurations.Add(configuration);
            return this;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("test");
            _configurations.ForEach(conf => builder.UseSetting(conf.Key, conf.Value));
        }
    }
}