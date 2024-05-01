// ASP.NET Core Web Application

namespace ChristCodingChallengeBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // curl -X 'GET' \ 'https://christ-coding-challenge.test.pub.k8s.christ.de/Article/GetArticles' \ -H 'accept: text/plain'
            // curl => Client URL = Befehlszeilen-Tool zum �bertragen von Daten mit URL-Syntax
            string apiUrl = "https://christ-coding-challenge.test.pub.k8s.christ.de/Article/GetArticles";

            // Speicherpfade
            string filePath = "articles.csv";
            string accessPath = "access.csv";

            // Builder Pattern => Konfiguration der WebApplication vor Initialisierung
            var builder = WebApplication.CreateBuilder(args);

            // CORS(Cross Origin Resource Sharing)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost4200",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // einheitliche Konfiguration der Protokollierung
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                // f�gt Konsolenausgabe-Konfiguration hinzu
                builder.AddConsole();
            });
            // gew�hrleistet Trennung der Log-Meldungen
            ILogger<ArticleApiService> articleApiLogger = new Logger<ArticleApiService>(loggerFactory);
            ILogger<StorageService> storageLogger = new Logger<StorageService>(loggerFactory);

            // Singleton Pattern => Solo-Instanz, globaler Zugriffspunkt
            var storageService = new StorageService(filePath, accessPath, storageLogger);
            builder.Services.AddSingleton(storageService);

            // Dependency Injection (DI) => ArticleApiService wird als Singleton registriert,
            // welcher den httpClient und storageService als Abh�ngigkeit verwendet
            HttpClient httpClient = new();
            builder.Services.AddSingleton<ArticleApiService>(new ArticleApiService(httpClient, apiUrl,
                storageService, articleApiLogger));

            // wichtig um Controller-Endpunkte zu aktivieren
            builder.Services.AddControllers();

            // Builder Pattern => Aufbau der WebApplication
            var app = builder.Build();

            // Wechseln f�r Produktiv / Entwicklung
            if (app.Environment.IsDevelopment())
            {
                // Fehlerbehandlungs-Middleware => leitet Fehler auf Error-Controller um
                //app.UseExceptionHandler("/Error");

                // Middleware f�r HTTP-Strict-Transport-Security => leitet HTTP-Anfragen auf HTTPS um
                //app.UseHsts();
            }

            // leitet HTTP-Anfragen auf HTTPS um
            //app.UseHttpsRedirection();
            // Middleware f�r statische Dateien => erm�glicht Bereitstellung von Dateien
            app.UseStaticFiles();
            // Middleware f�r Endpunkt-Routing => leitet Anfragen an Endpunkte weiter
            app.UseRouting();
            // CORS: allow localhost:4200
            app.UseCors("AllowLocalhost4200");
            // Controller-Endpunkte aktivieren
            app.UseEndpoints(endpoints =>
            {
                // Hier registrierst du deine Controller-Endpunkte
                endpoints.MapControllers();
            });
            // Middleware f�r Authentifizierung => leitet Anfragen an Authentifizierungsdienst weiter
            //app.UseAuthorization();
            // Middleware f�r Endpunkte => Fallback-Handler f�r nicht abgefangene Anfragen
            app.Run();
        }
    }
}
