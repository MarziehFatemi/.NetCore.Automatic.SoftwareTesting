namespace Onion.Test.Acceptance.Core
{
    public interface IStartableHost : IHost
    {
        void Start();
        void Stop();
    }
}