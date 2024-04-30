using System.Text.Json.Serialization;

namespace ChristCodingChallengeBackend
{
    public class JsonArticle
    {
        // benutzerdefiniertes Attribut => zeigt z.B. De/Serialisierer an, wie Eigenschaft zu behandeln ist
        [JsonPropertyName("articleId")]
        public string ArticleId { get; set; } = string.Empty;

        [JsonPropertyName("artikelnummer")]
        public string Artikelnummer { get; set; } = string.Empty;
        [JsonPropertyName("marke")]
        public string Marke { get; set; } = string.Empty;
        [JsonPropertyName("material1")]
        public string Material1 { get; set; } = string.Empty;
        [JsonPropertyName("legierung1")]
        public string Legierung1 { get; set; } = string.Empty;
        [JsonPropertyName("kollektion")]
        public string Kollektion { get; set; } = string.Empty;
        [JsonPropertyName("warengruppe")]
        public string Warengruppe { get; set; } = string.Empty;
        [JsonPropertyName("warenhauptgruppe")]
        public string Warenhauptgruppe { get; set; } = string.Empty;
        [JsonPropertyName("geschlecht")]
        public string Geschlecht { get; set; } = string.Empty;

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
