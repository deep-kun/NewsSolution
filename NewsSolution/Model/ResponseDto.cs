using System;
using Newtonsoft.Json;

namespace NewsSolution.Model
{
    public partial class ResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("news")]
        public News[] News { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }
    }

    public partial class News
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("image")]
        public ImageUnion Image { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("category")]
        public string[] Category { get; set; }

        [JsonProperty("published")]
        public string Published { get; set; }
    }

    public enum ImageEnum { None };

    public enum Language { En };

    public partial struct ImageUnion
    {
        public ImageEnum? Enum;
        public Uri PurpleUri;

        public static implicit operator ImageUnion(ImageEnum Enum) => new ImageUnion { Enum = Enum };
        public static implicit operator ImageUnion(Uri PurpleUri) => new ImageUnion { PurpleUri = PurpleUri };
    }

    internal class ImageUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ImageUnion) || t == typeof(ImageUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    if (stringValue == "None")
                    {
                        return new ImageUnion { Enum = ImageEnum.None };
                    }
                    try
                    {
                        var uri = new Uri(stringValue);
                        return new ImageUnion { PurpleUri = uri };
                    }
                    catch (UriFormatException) { }
                    break;
            }
            throw new Exception("Cannot unmarshal type ImageUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ImageUnion)untypedValue;
            if (value.Enum != null)
            {
                if (value.Enum == ImageEnum.None)
                {
                    serializer.Serialize(writer, "None");
                    return;
                }
            }
            if (value.PurpleUri != null)
            {
                serializer.Serialize(writer, value.PurpleUri.ToString());
                return;
            }
            throw new Exception("Cannot marshal type ImageUnion");
        }

        public static readonly ImageUnionConverter Singleton = new ImageUnionConverter();
    }

    internal class ImageEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ImageEnum) || t == typeof(ImageEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "None")
            {
                return ImageEnum.None;
            }
            throw new Exception("Cannot unmarshal type ImageEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ImageEnum)untypedValue;
            if (value == ImageEnum.None)
            {
                serializer.Serialize(writer, "None");
                return;
            }
            throw new Exception("Cannot marshal type ImageEnum");
        }

        public static readonly ImageEnumConverter Singleton = new ImageEnumConverter();
    }

    internal class LanguageConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Language) || t == typeof(Language?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            //if (value == "en")
            {
                return Language.En;
            }
            throw new Exception("Cannot unmarshal type Language");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Language)untypedValue;
            if (value == Language.En)
            {
                serializer.Serialize(writer, "en");
                return;
            }
            throw new Exception("Cannot marshal type Language");
        }

        public static readonly LanguageConverter Singleton = new LanguageConverter();
    }
}
