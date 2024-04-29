namespace ChristCodingChallengeBackend
{
    public class ArticleApiService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly StorageService _storageService;
        private readonly ILogger<ArticleApiService> _logger;
        private readonly string _apiUrl;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public ArticleApiService(HttpClient httpClient, string apiUrl, StorageService storageService, ILogger<
            ArticleApiService> logger)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
            _storageService = storageService;
            _logger = logger;

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
            while (true)
            {
                try
                {
                    // API-Call
                    string result = await GetArticlesAsync();
                    _logger.LogInformation("Result: {Result}", string.Concat(result.AsSpan(0, 200), " [...]"));
                    _logger.LogInformation("ResultLength: {ResultLength}", result.Length);

                    // Datenverarbeitung in StorageService-Dienst
                    //await _storageService.StoreArticlesAsync(result);
                    if (!string.IsNullOrEmpty(result)) _storageService.StoreArticlesAsync(result);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Periodic API-Call failed: {Exception}", ex.Message);
                    //throw new Exception($"Periodic API-Call failed: {ex.Message}");
                }
                // 5 Minuten Verzögerung
                await Task.Delay(300 * 1000);
            }
        }

        public async Task<string> GetArticlesAsync()
        {
            // curl -X 'GET' => GetAsync() sendet standardmäßig GET-Requests
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
            _logger.LogInformation("API request to {Target}", _apiUrl);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("API request successful with status code {Status}", response.StatusCode);
                // response.IsSuccessStatusCode prüft nicht, ob Antwort vollständig verfügbar
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogInformation("API request failed with status code {Status}", response.StatusCode);
                //throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
                return string.Empty;
            }
        }
        #endregion
    }
}
