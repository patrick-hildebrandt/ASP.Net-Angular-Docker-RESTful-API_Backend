namespace ChristCodingChallengeBackend
{
    public class ArticleApiService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly StorageService _storageService;
        private readonly string _apiUrl;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public ArticleApiService(HttpClient httpClient, string apiUrl, StorageService storageService)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
            _storageService = storageService;

            // Accept-Header explizit auf "text/plain" setzen
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(
                "text/plain"));
            
            // Ignorieren von Rückgabe-Task => Hauptprozess wird nicht blockiert
            _ = PeriodicApiCallAsync();
        }
        #endregion

        #region Methods
        private async Task PeriodicApiCallAsync()
        {
            try
            {
                while (true)
                {
                    // API-Call
                    string result = await GetArticlesAsync();

                    // Datenverarbeitung in StorageService-Dienst
                    await _storageService.StoreArticlesAsync(result);

                    // 5 Minuten Verzögerung
                    await Task.Delay(300 * 1000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Periodic API-Call failed with error message {ex.Message}");
            }
        }

        public async Task<string> GetArticlesAsync()
        {
            // curl -X 'GET' => GetAsync() sendet standardmäßig GET-Requests
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // response.IsSuccessStatusCode prüft nicht, ob Antwort vollständig verfügbar
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
            }
        }
        #endregion
    }
}
