using System.Net;

namespace Sample0_LogonWithProxy
{
    public static class Proxy
    {
        public static IWebProxy Get( ProxyData proxyData )
        {
            return new WebProxy( proxyData.Address, proxyData.Port )
            {
                Credentials = new NetworkCredential( proxyData.Username, proxyData.Password )
            };
        }

    }
}
