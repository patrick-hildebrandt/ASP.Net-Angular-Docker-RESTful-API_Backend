using System.Text.Json;

namespace ChristCodingChallengeBackend
{
    public class StorageService(string filePath, string accessPath)
    {
        #region Fields
        // Hauptdateipfad
        private readonly string _filePath = filePath;
        // Zugriffsdateipfad
        private readonly string _accessPath = accessPath;
        // JsonArticle => Article
        private readonly Dictionary<string, Article> _articles = [];
        // relevante Attribute
        private readonly Dictionary<string, string> _relevantAttributes = new()
        {
            { "MRK", "Marke" },
            { "MAT", "Material1" },
            { "MAT2", "Material2" },
            { "MAT3", "Material3" },
            { "LEG", "Legierung1" },
            { "LEG2", "Legierung2" },
            { "LEG3", "Legierung3" },
            { "KOLL", "Kollektion" },
            { "WRG_2", "Warengruppe" },
            { "WHG_2", "Warenhauptgruppe" },
            { "ZIEL", "Geschlecht" }
        };
        #endregion

        #region Constructors

        #endregion

        #region Methods
        public async Task StoreArticlesAsync(string result)
        {
            await ParseArticlesFromJson(result);

            await StoreArticlesToCsv(_articles);
        }

        private async Task ParseArticlesFromJson(string result)
        {
            // Deserialisierung
            var articles = JsonSerializer.Deserialize<JsonArticle[]>(result);

            // Verarbeitung von Artikeln
            if (articles != null)
            {
                foreach (var article in articles)
                {
                    if (!_articles.ContainsKey(article.ArticleId.ToString()))
                    {
                        _articles.Add(article.ArticleId.ToString(), new Article(article.ArticleId));
                    }
                    foreach (var relevantAttribute in _relevantAttributes)
                    {
                        bool found = false;

                        foreach (var attribute in article.Attributes)
                        {
                            if (attribute.Key == relevantAttribute.Key && attribute.Language == "de")
                            {
                                _articles[article.ArticleId.ToString()].GetType().GetProperty(relevantAttribute.Value)
                                    ?.SetValue(_articles[article.ArticleId.ToString()], attribute.Value);

                                found = true;
                                break;
                            }
                        }
                        if (found) continue;
                    }
                }
            }
        }

        private async Task StoreArticlesToCsv(Dictionary<string, Article> articles)
        {
            using (StreamWriter writer = new(_filePath))
            {
                // Header schreiben
                WriteHeader(writer);

                // Zeilen schreiben
                foreach (var article in articles)
                {
                    await writer.WriteLineAsync(
                        $"{article.Value.Artikelnummer};" +
                        $"{article.Value.Marke};" +
                        $"{article.Value.Material1};" +
                        $"{article.Value.Material2};" +
                        $"{article.Value.Material3};" +
                        $"{article.Value.Legierung1};" +
                        $"{article.Value.Legierung2};" +
                        $"{article.Value.Legierung3};" +
                        $"{article.Value.Kollektion};" +
                        $"{article.Value.Warengruppe};" +
                        $"{article.Value.Warenhauptgruppe};" +
                        $"{article.Value.Geschlecht}"
                        );
                }
            }
            // Kopie der Hauptdatei für Zugriff
            CreateAccessFile();
        }

        public void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("Artikelnummer;Marke;Material1;Material2;Material3;Legierung1;Legierung2;Legierung3;" +
                "Kollektion;Warengruppe;Warenhauptgruppe;Geschlecht");
        }

        private void CreateAccessFile()
        {
            if (File.Exists(_filePath))
            {
                if (File.Exists(_accessPath))
                {
                    File.Delete(_accessPath);
                }
                File.Copy(_filePath, _accessPath);
            }
        }
        #endregion
    }
}
