using Confluent.Kafka;
using System;

namespace NewsConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var topic = "news";

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topic);

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    var messageValue = consumeResult.Message.Value;
                    Console.WriteLine($"Recived {DateTime.Now}");
                    Console.WriteLine(messageValue);
                }
                consumer.Close();
            }
        }
    }
}
