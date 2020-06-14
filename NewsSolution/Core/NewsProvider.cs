using Microsoft.Extensions.Options;
using NewsSolution.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Globalization;

namespace NewsSolution.Core
{
    public class NewsProvider : INewsProvider
    {
        private const string latestNewsUrl = "https://api.currentsapi.services/v1/latest-news";

        private readonly IRestClient restClient;

        public NewsProvider(IOptions<AppSettings> options)
        {
            restClient = new RestClient();
            restClient.UseNewtonsoftJson(new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                            {
                                ImageUnionConverter.Singleton,
                                ImageEnumConverter.Singleton,
                                LanguageConverter.Singleton,
                                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                            }
            });

            restClient.AddDefaultHeader("Authorization", options.Value.ApiSecret);
        }

        public ResponseDto GetData()
        {
            var request = new RestRequest(latestNewsUrl, Method.GET);
            var res = restClient.Execute<ResponseDto>(request);
            return res.Data;
        }
    }
}
