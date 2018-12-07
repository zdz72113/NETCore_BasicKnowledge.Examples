using EasyNetQ;
using System;
using TestRabbitMQ.Message;

namespace TestRabbitMQ.Publisher
{
    class Publisher
    {
        static void Main(string[] args)
        {
            Console.Title = "TestRabbitMQ.Publisher";

            using (var bus = RabbitHutch.CreateBus("host=192.168.17.129"))
            {
                do
                {
                    Console.WriteLine("Please enter your message, if want to exit please press q.");
                    string message = Console.ReadLine();
                    if (message.ToLower().Equals("q"))
                    {
                        break;
                    }

                    bus.Publish(new DemoMessage
                    {
                        Id = Guid.NewGuid(),
                        Text = message,
                        CreatedTime = DateTime.Now
                    });
                } while (true);
            }
        }
    }
}
