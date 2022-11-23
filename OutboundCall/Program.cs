using System.Collections.Specialized;
using System.Configuration;

namespace OutboundCall
{
    public class OutboundCall
    {
        static void Main(string[] args)
        {
            Log.Logger.Info("Application starting ... .");


            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", @"..\App.config");

            API.InitializeApis();
            Configure.RunConfig();

            
            // Task for updating OAuth token and clearing contact lists
            Task.Run(() =>
            {
                int count = int.Parse(ConfigurationManager.AppSettings["contactListPeriod"]);
                while (true)
                {
                    count--;
                    Thread.Sleep(86400 * 1000);
                    Configure.RunConfig();
                    if (count == 0)
                    {
                        count = int.Parse(ConfigurationManager.AppSettings["contactListPeriod"]);
                        Contacts.ClearContacts();
                    }
                }
            });

            WebsocketResponse.RunWebSocket();
            Console.ReadLine();
        }    
    }
}
