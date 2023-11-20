using Newtonsoft.Json;

namespace Sample0_LogonWithProxy
{
    public class Program
    {
        private const string LogonDataPath = "logon_data.json";

        static void Main( string[] args )
        {
            var logonData = ReadLogonData();
            Console.WriteLine( logonData.Username + " " + logonData.Password );
            Console.ReadKey();
        }

        private static LogonData ReadLogonData()
        {
            using StreamReader reader = new( LogonDataPath );
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<LogonData>( json );
        }
    }
}
