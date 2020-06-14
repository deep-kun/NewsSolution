using System;
using System.Threading.Tasks;

namespace NewsSolution.Core
{
    class App
    {
        private readonly INewsProvider newsProvider;

        public App(INewsProvider newsProvider)
        {
            this.newsProvider = newsProvider;
        }

        public async Task Run(string[] args)
        {
            var result = newsProvider.GetData();

            foreach (var item in result.News)
            {
                Console.WriteLine(item.Title);
            }

            Console.WriteLine(result.News.Length);

            Console.ReadLine();

            await Task.CompletedTask;
        }
    }
}
