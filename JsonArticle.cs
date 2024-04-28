using System.Text.Json.Serialization;

namespace ChristCodingChallengeBackend
{
    public class JsonArticle
    {
        // benutzerdefiniertes Attribut => zeigt z.B. De/Serialisierer an, wie Eigenschaft zu behandeln ist
        [JsonPropertyName("articleId")]
        public string ArticleId { get; set; } = string.Empty;

        [JsonPropertyName("attributes")]
        public List<JsonAttribute> Attributes { get; set; } = [];
    }

    public class JsonAttribute
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;
    }
}
