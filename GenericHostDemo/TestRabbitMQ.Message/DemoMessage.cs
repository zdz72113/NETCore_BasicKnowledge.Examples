using System;

namespace TestRabbitMQ.Message
{
    public class DemoMessage
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
