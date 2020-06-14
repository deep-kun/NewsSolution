using Confluent.Kafka;
using Microsoft.Extensions.Options;
using News.Models;
using Newtonsoft.Json;
using System.Net;

namespace NewsSolution.Core
{
    class KafkaProducer
    {
        private readonly AppSettings options;

        public KafkaProducer(IOptions<AppSettings> options)
        {
            this.options = options.Value;
        }

        public void PostMessage(NewsItem[] newsItems)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = options.BootstrapServer,
                ClientId = Dns.GetHostName(),
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                foreach (var item in newsItems)
                {
                    var message = new Message<Null, string>
                    {
                        Value = JsonConvert.SerializeObject(item)
                    };

                    producer.Produce(options.Topic, message);
                }
            }
        }
    }
}
