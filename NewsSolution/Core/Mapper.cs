using NewsSolution.Model;

namespace NewsSolution.Core
{
    static class Mapper
    {
        public static NewsItem MapNewsDtoToNewsItem(News news) => new NewsItem
        {
            Author = news.Author,
            Category = news.Category,
            Description = news.Description,
            Id = news.Id,
            Image = news.Image.PurpleUri,
            Published = news.Published,
            Title = news.Title,
            Url = news.Url,
        };
    }
}
