namespace SampleSender
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs;
    using System.Linq;

    public class Program
    {
        private static EventHubClient eventHubClient;
        private const string EhConnectionString = "Endpoint=sb://eventsevg.servicebus.windows.net/;SharedAccessKeyName=sender;SharedAccessKey=f1usoK0raySXt/xkbhkdDveahmmIHlSEmFdu4JiL58I=;EntityPath=mugkatcher";
        private const string EhEntityPath = "mugkatcher";

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            // Creates an EventHubsConnectionStringBuilder object from a the connection string, and sets the EntityPath.
            // Typically the connection string should have the Entity Path in it, but for the sake of this simple scenario
            // we are using the connection string from the namespace.
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            var rec = eventHubClient.CreateReceiver("$Default","0",new DateTime(2017/02/26));


              await SendMessagesToEventHub(100);
            int idxread=0;
            while (true) {
                
                var t = await rec.ReceiveAsync(100);
                if (t==null || t.Count() == 0) break;
                foreach (var a in t)
                {
                    idxread++;
                    Console.WriteLine("idxread:"+ idxread +" *->"+ Encoding.UTF8.GetString(a.Body.Array));
                }
            }
            Console.WriteLine("ya sali de la cola.");
            await eventHubClient.CloseAsync();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        // Creates an Event Hub client and sends 100 messages to the event hub.
        private static async Task SendMessagesToEventHub(int numMessagesToSend)
        {
            var td = DateTime.UtcNow;
            for (var i = 0; i < numMessagesToSend; i++)
            {
                try
                {
                    var message = $"byr /// Message {i} "+ td;
                    Console.WriteLine($"Sending message: {message}");
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
    }
}