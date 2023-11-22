using System.Net;

namespace Sample0_LogonWithProxy
{
    public static class ProxyBuilder
    {
        public static WebProxy Build( ProxyData proxyData )
        {
            return new WebProxy( proxyData.Address, proxyData.Port )
            {
                Credentials = new NetworkCredential( proxyData.Username, proxyData.Password )
            };
        }

    }
}
