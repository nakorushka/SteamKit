using Newtonsoft.Json;
using SteamAuthCore;

namespace Sample0_LogonWithProxy
{
    public static class DataReader
    {
        const string LogonDataPath = "logon_data.json";
        const string ProxyDataPath = "proxy_data.json";
        const string SteamGuardAccountPath = "steam_guard_account.maFile";

        public static LogonData ReadLogonData()
        {
            using StreamReader reader = new( LogonDataPath );
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<LogonData>( json );
        }

        public static ProxyData ReadProxyData()
        {
            using StreamReader reader = new( ProxyDataPath );
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<ProxyData>( json );
        }

        public static SteamGuardAccount ReadSteamGuardAccount()
        {
            using StreamReader reader = new( SteamGuardAccountPath );
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<SteamGuardAccount>( json );
        }
    }
}
