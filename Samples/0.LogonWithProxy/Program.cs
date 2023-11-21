using Sample0_LogonWithProxy;
using SteamKit2;

var logonData = DataReader.ReadLogonData();
var proxyData = DataReader.ReadProxyData();

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
var proxy = Proxy.Get( proxyData );
steamClient.Connect( proxy: proxy );

// create our callback handling loop
while ( isRunning )
{
    // in order for the callbacks to get routed, they need to be handled by the manager
    manager.RunWaitCallbacks( TimeSpan.FromSeconds( 1 ) );
}

Console.ReadKey();

void OnConnected( SteamClient.ConnectedCallback callback )
{
    Console.WriteLine( "Connected to Steam! Logging in '{0}'...", logonData.Username );

    steamUser.LogOn( new SteamUser.LogOnDetails
    {
        Username = logonData.Username,
        Password = logonData.Password,
    } );
}

void OnDisconnected( SteamClient.DisconnectedCallback callback )
{
    Console.WriteLine( "Disconnected from Steam" );

    isRunning = false;
}

void OnLoggedOn( SteamUser.LoggedOnCallback callback )
{
    if ( callback.Result != EResult.OK )
    {
        if ( callback.Result == EResult.AccountLogonDenied )
        {
            // if we recieve AccountLogonDenied or one of it's flavors (AccountLogonDeniedNoMailSent, etc)
            // then the account we're logging into is SteamGuard protected
            // see sample 5 for how SteamGuard can be handled

            Console.WriteLine( "Unable to logon to Steam: This account is SteamGuard protected." );

            isRunning = false;
            return;
        }

        Console.WriteLine( "Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult );

        isRunning = false;
        return;
    }

    Console.WriteLine( "Successfully logged on!" );

    // at this point, we'd be able to perform actions on Steam

    // for this sample we'll just log off
    steamUser.LogOff();
}

void OnLoggedOff( SteamUser.LoggedOffCallback callback )
{
    Console.WriteLine( "Logged off of Steam: {0}", callback.Result );
}
