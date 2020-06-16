using NewsSolution.Model;

namespace NewsSolution.Core
{
    interface IKafkaProducer
    {
        void PostMessage(NewsItem[] newsItems);
    }
}