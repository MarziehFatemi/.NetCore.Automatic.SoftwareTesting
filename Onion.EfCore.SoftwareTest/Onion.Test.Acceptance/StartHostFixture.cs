using System;
using Onion.Test.Acceptance.Core;
using Onion.Test.Acceptance.NetCoreHosting;

namespace Onion.Test.Acceptance
{
    public class StartHostFixture : IDisposable
    {
        private readonly IStartableHost _host = new DotNetCoreHost(new DotNetCoreHostOptions
        {
            Port = HostConstants.Port,
            CsProjectPath = HostConstants.CsProjectPath
        });

        public StartHostFixture()
        {
            _host.Start();
        }

        public void Dispose()
        {
            _host.Stop();
        }
    }
}