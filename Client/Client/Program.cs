using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;


namespace Client
{
   public class Program
    {
        static Microsoft.Azure.ServiceBus.QueueClient queueClient;
        static string s;
        static void Main(string[] args)
        {
            string sbConnectionString = "Endpoint=sb://miniproject.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tGkzmzkJl5PqEBSAQxiWA92Dt0wV9HSBOh7QS6k4OGo=";
            string sbQueName = "recharge";
            string messageBody = string.Empty;
            do
            {
            try
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Mobile Recharge information");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Mobile operator list");
                Console.WriteLine("1. Greemen phone");
                Console.WriteLine("2. Aktel");
                Console.WriteLine("3. Banglalink");
                Console.WriteLine("4. Citycell");
                Console.WriteLine("----------------------------------------------------");

                Console.WriteLine("Choose the operator to recharge:");
                string mobileOperator = Console.ReadLine();
                Console.WriteLine("Recharge amount:");
                string amount = Console.ReadLine();
                Console.WriteLine("Enter mobile number");
                string moileNo = Console.ReadLine();
                Console.WriteLine("----------------------------------------------------");

                switch (mobileOperator)
                {
                    case "1":
                        mobileOperator = "Greemen phone";
                        break;

                    case "2":
                        mobileOperator = "Aktel";
                        break;

                    case "3":
                        mobileOperator = "Banglalink";
                        break;

                    case "4":
                        mobileOperator = "Citycell";
                        break;

                    default:
                        break;
                }

                messageBody ="Operator Name:"+ mobileOperator + "\n Phone No:" + moileNo + "\n Amount:" + amount ;
                queueClient =new Microsoft.Azure.ServiceBus.QueueClient(sbConnectionString, sbQueName);

                var message = new Microsoft.Azure.ServiceBus.Message(Encoding.UTF8.GetBytes(messageBody));
                Console.WriteLine($"Message Added in Queue: {messageBody}");
                queueClient.SendAsync(message);

               

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
                Console.WriteLine("Do you want to send more message: y/n");
                s = Console.ReadLine().ToString();
                Console.ReadKey();
            }while (s == "y");
        }
    }
}
