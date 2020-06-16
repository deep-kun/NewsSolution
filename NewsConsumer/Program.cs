using Confluent.Kafka;
using System;
using System.Data.SqlClient;
using Dapper;
using System.Runtime.CompilerServices;

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
                    PutDataIntoDb(messageValue);
                    Console.WriteLine($"Recived {DateTime.Now}");
                    Console.WriteLine(messageValue);
                }
                consumer.Close();
            }
        }

        private static void PutDataIntoDb(string data)
        {
            using (var sqlConnection = new SqlConnection("Server=db;Database=News;User=sa;Password=Your_password123;"))
            {
                sqlConnection.Open();
                sqlConnection.Query($"insert into news values('{data}')");
            }
        }
    }
}
