using evgCoreApi.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace evgEvents
{
    public class evgEvents
    {
        private static EventHubClient eventHubClient=null;
        private const string EhConnectionString = "Endpoint=sb://eventsevg.servicebus.windows.net/;SharedAccessKeyName=sender;SharedAccessKey=f1usoK0raySXt/xkbhkdDveahmmIHlSEmFdu4JiL58I=;EntityPath=mugkatcher";
        private const string EhEntityPath = "mugkatcher";

        public static async Task MainAsync(string[] args)
        {
            // Creates an EventHubsConnectionStringBuilder object from a the connection string, and sets the EntityPath.
            // Typically the connection string should have the Entity Path in it, but for the sake of this simple scenario
            // we are using the connection string from the namespace.
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath

            }
            ;


            if (eventHubClient == null) { 
              eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            }





            await SendMessagesToEventHub(1);

            //** await eventHubClient. CloseAsync();

            Console.WriteLine("Press any key to exit.");
            //***Console.ReadLine();
        }

        // Creates an Event Hub client and sends 100 messages to the event hub.
        private static async Task SendMessagesToEventHub(int numMessagesToSend)
        {
            for (var i = 0; i < numMessagesToSend; i++)
            {
                try
                {
                    var message = $"Message byr news {i}";
                    Console.WriteLine($"Sending message byr news: {message}");
                    
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                }

              //**  await Task.Delay(10);
            }

            Console.WriteLine($"{numMessagesToSend} messages sent.");
        }

        public static void SendEvent(model_session sess)
        {
            var ret = JsonConvert.SerializeObject(sess);
            MainAsync(new string[] { ret }).GetAwaiter().GetResult();

        }
    }
}
