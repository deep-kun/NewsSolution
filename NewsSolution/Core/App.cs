using NewsSolution.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsSolution.Core
{
    class App
    {
        private readonly INewsProvider newsProvider;
        private readonly IKafkaProducer kafkaProducer;

        public App(INewsProvider newsProvider, IKafkaProducer  kafkaProducer)
        {
            this.newsProvider = newsProvider;
            this.kafkaProducer = kafkaProducer;
        }

        public async Task Run(string[] args)
        {
            while (true)
            {
                //var result = newsProvider.GetData();

                var result = new[]{
                    new News
                {
                    Title = "title"
                }};
                //this.kafkaProducer.PostMessage(result.News.Select(Mapper.MapNewsDtoToNewsItem).ToArray());
                this.kafkaProducer.PostMessage(result.Select(Mapper.MapNewsDtoToNewsItem).ToArray());
                Console.WriteLine("Date now = " + DateTime.Now);
                Console.WriteLine(DateTime.Now);
                Thread.Sleep(15000);
            }

            await Task.CompletedTask;
        }
    }
}
