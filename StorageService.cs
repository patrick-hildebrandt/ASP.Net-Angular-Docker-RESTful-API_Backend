namespace ChristCodingChallengeBackend
{
    public class StorageService
    {
        // ! Fields
        // Hauptdateipfad
        private readonly string _filePath;
        // Zugriffsdateipfad
        private readonly string _accessPath;

        // ! Properties

        // ! Constructors
        public StorageService(string filePath, string accessPath)
        {
            _filePath = filePath;
            _accessPath = accessPath;
        }

        // ! Methods
        // todo Better Comments entfernen
        // todo HIER WEITER
        public async Task StoreArticlesAsync(IEnumerable<Article> articles)
        {
            using (StreamWriter writer = new(_filePath))
            {
                // Header schreiben
                WriteHeader(writer);

                // Zeilen schreiben
                foreach (var article in articles)
                {
                    await writer.WriteLineAsync($"{article.Artikelnummer};{article.Marke};{article.Material1};" +
                        $"{article.Material2};{article.Material3};{article.Legierung1};{article.Legierung2};" +
                        $"{article.Legierung3};{article.Kollektion};{article.Warengruppe};{article.Warenhauptgruppe}" +
                        $";{article.Geschlecht}");
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
    }
}
