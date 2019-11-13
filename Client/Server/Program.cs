using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Threading;

namespace Server
{
    public class Program
    {
        static Microsoft.Azure.ServiceBus.QueueClient queueClient;
        static void Main(string[] args)
        {
            string sbConnectionString = "Endpoint=sb://miniproject.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tGkzmzkJl5PqEBSAQxiWA92Dt0wV9HSBOh7QS6k4OGo=";
            string sbQueName = "recharge";

            try
            {
                queueClient = new Microsoft.Azure.ServiceBus.QueueClient(sbConnectionString, sbQueName);


                var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };
                queueClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                queueClient.CloseAsync();
                
            }
            
        }

        static async Task ReceiveMessagesAsync(Microsoft.Azure.ServiceBus.Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(Microsoft.Azure.ServiceBus.ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }


    }
}





