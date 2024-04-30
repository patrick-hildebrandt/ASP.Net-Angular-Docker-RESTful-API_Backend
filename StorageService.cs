using System.Text.Json;
using CsvHelper;
using System.Globalization;

namespace ChristCodingChallengeBackend
{
    public class StorageService(string filePath, string accessPath, ILogger<StorageService> logger)
    {
        #region Fields
        private readonly ILogger<StorageService> _logger = logger;
        // Hauptdateipfad
        private readonly string _filePath = filePath;
        // Zugriffsdateipfad
        private readonly string _accessPath = accessPath;
        // JsonArticle => Article
        private readonly Dictionary<string, Article> _articles = [];
        // relevante Attribute
        private readonly Dictionary<string, string> _relevantAttributes = new()
        {
            // tatsächliche Attribute:
            // articleId
            // Artikelnummer - MITMAS_ITNO
            // Marke - MRK
            // Material 1 - MAT
            // Legierung 1 - LEG
            // Kollektionsjahr - MITMAS_CFI4
            // Warengruppe - WRG_2
            // Warenhauptgruppe - WHG_2
            // Zielgruppe - Ziel

            { "MRK", "Marke" },
            { "MAT", "Material1" },
            { "MAT2", "Material2" },
            { "MAT3", "Material3" },
            { "LEG", "Legierung1" },
            { "LEG2", "Legierung2" },
            { "LEG3", "Legierung3" },
            //{ "KOLL", "Kollektion" },
            { "MITMAS_CFI4", "Kollektion" },
            { "WRG_2", "Warengruppe" },
            { "WHG_2", "Warenhauptgruppe" },
            { "ZIEL", "Geschlecht" }
        };
        #endregion

        #region Constructors

        #endregion

        #region Methods
        public List<JsonArticle> GetArticlesJson()
        {
            using var reader = new StreamReader(_accessPath);
            using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            });

            csv.Read();
            csv.ReadHeader();

            var articles = new List<JsonArticle>();

            while (csv.Read())
            {
                var article = new JsonArticle
                {
                    Artikelnummer = csv.GetField("Artikelnummer"),
                    Marke = csv.GetField("Marke"),
                    Material1 = csv.GetField("Material1"),
                    Legierung1 = csv.GetField("Legierung1"),
                    Kollektion = csv.GetField("Kollektion"),
                    Warengruppe = csv.GetField("Warengruppe"),
                    Warenhauptgruppe = csv.GetField("Warenhauptgruppe"),
                    Geschlecht = csv.GetField("Geschlecht")
                };

                articles.Add(article);
            }
            return articles;
        }

        public void StoreArticles(string result)
        {
            ParseArticlesFromJson(result);

            StoreArticlesToCsv(_articles);

            //var test = GetArticlesJson();
            //foreach (var article in test)
            //{
            //    _logger.LogInformation("ArticleId: {ArticleId}", article.ArticleId);
            //}
        }

        private void ParseArticlesFromJson(string result)
        {
            // Deserialisierung
            var articles = JsonSerializer.Deserialize<JsonArticle[]>(result);

            // Verarbeitung von Artikeln
            if (articles != null)
            {
                foreach (var article in articles)
                {
                    // todo ggf. Update-Strategie überarbeiten mit DB oder Docker-Lösung ?
                    // Umsetzung der Update-Strategie
                    if (!_articles.ContainsKey(article.ArticleId))
                    {
                        _articles.Add(article.ArticleId, new Article(article.ArticleId));
                    }
                    // todo ggf. doch out-Parameter verwenden für Performance ? Semantik korrekt ?
                    //if (!_articles.TryGetValue(article.ArticleId, out Article? value))
                    //{
                    //    value = new Article(article.ArticleId);
                    //    _articles.Add(article.ArticleId, value);
                    //}
                    foreach (var relevantAttribute in _relevantAttributes)
                    {
                        bool found = false;

                        foreach (var attribute in article.Attributes)
                        {
                            if (attribute.Key == relevantAttribute.Key && attribute.Language == "de" ||
                                attribute.Key == relevantAttribute.Key && attribute.Language == null)
                            {
                                // Umsetzung der Update-Strategie
                                if (_articles[article.ArticleId].GetType().GetProperty(relevantAttribute.Value)?
                                    .ToString() != attribute.Value)
                                {
                                    // lol
                                    _articles[article.ArticleId].GetType().GetProperty(relevantAttribute.Value)?
                                        .SetValue(_articles[article.ArticleId], attribute.Value);
                                }
                                // todo ggf. doch out-Parameter verwenden für Performance ? Semantik korrekt ?
                                //value.GetType().GetProperty(relevantAttribute.Value)?.SetValue(value, attribute.Value);

                                found = true;
                                break;
                            }
                        }
                        if (found) continue;
                    }
                }
                //foreach (var article in _articles)
                //{
                //    _logger.LogInformation("ArticleId: {ArticleId}", article.Key);
                //    //foreach (var property in article.Value.GetType().GetProperties())
                //    //{
                //    //    _logger.LogInformation("{Property}: {Value}", property.Name, property.GetValue(article.Value));
                //    //}
                //}
            }
        }

        private void StoreArticlesToCsv(Dictionary<string, Article> articles)
        {
            using (StreamWriter writer = new(_filePath))
            {
                // Header schreiben
                WriteHeader(writer);

                // Zeilen schreiben
                foreach (var article in articles)
                {
                    writer.WriteLine(article.Value.ToCsv());
                }
                Console.WriteLine("Articles-File wurde geschrieben");
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
                try
                {
                    // Versuch Datei zu kopieren
                    if (File.Exists(_accessPath))
                    {
                        File.Delete(_accessPath);
                    }
                    File.Copy(_filePath, _accessPath);
                    Console.WriteLine("Access-File wurde geschrieben");
                }
                catch (IOException ex)
                {
                    // Datei ist bereits geöffnet
                    _logger.LogInformation("Access-File ist bereits geöffnet (IOException: {Exception})", ex.Message);
                }
            }
        }
        #endregion
    }
}
