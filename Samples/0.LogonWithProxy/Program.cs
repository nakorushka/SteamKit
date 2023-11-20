using Newtonsoft.Json;
using SteamKit2;

namespace Sample0_LogonWithProxy
{
    public class Program
    {
        private const string LogonDataPath = "logon_data.json";

        static void Main( string[] args )
        {
            var logonData = ReadLogonData();
            string previouslyStoredGuardData = null;

            if ( string.IsNullOrEmpty( logonData.Username ) || string.IsNullOrEmpty( logonData.Password ) )
            {
                Console.Error.WriteLine( "Username or password is empty!" );
                return;
            }

            // create our steamclient instance
            var steamClient = new SteamClient();
            // create the callback manager which will route callbacks to function calls
            var manager = new CallbackManager( steamClient );

            // get the steamuser handler, which is used for logging on after successfully connecting
            var steamUser = steamClient.GetHandler<SteamUser>();

            // register a few callbacks we're interested in
            // these are registered upon creation to a callback manager, which will then route the callbacks
            // to the functions specified
            manager.Subscribe<SteamClient.ConnectedCallback>( OnConnected );
            manager.Subscribe<SteamClient.DisconnectedCallback>( OnDisconnected );

            manager.Subscribe<SteamUser.LoggedOnCallback>( OnLoggedOn );
            manager.Subscribe<SteamUser.LoggedOffCallback>( OnLoggedOff );

            var isRunning = true;

            Console.WriteLine( "Connecting to Steam..." );

            // initiate the connection
            steamClient.Connect();

            // create our callback handling loop
            while ( isRunning )
            {
                // in order for the callbacks to get routed, they need to be handled by the manager
                manager.RunWaitCallbacks( TimeSpan.FromSeconds( 1 ) );
            }
        }

        private static LogonData ReadLogonData()
        {
            using StreamReader reader = new( LogonDataPath );
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<LogonData>( json );
        }
    }
}
