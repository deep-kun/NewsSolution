using News.Models;

namespace NewsSolution.Core
{
    interface IKafkaProducer
    {
        void PostMessage(NewsItem[] newsItems);
    }
}