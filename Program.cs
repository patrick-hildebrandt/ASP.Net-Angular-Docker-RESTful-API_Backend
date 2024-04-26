// ASP.NET Core Web Application
namespace ChristCodingChallengeBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // curl -X 'GET' \ 'https://christ-coding-challenge.test.pub.k8s.christ.de/Article/GetArticles' \ -H 'accept: text/plain'
            // curl => Client URL = Befehlszeilen-Tool zum Übertragen von Daten mit URL-Syntax
            string apiUrl = "https://christ-coding-challenge.test.pub.k8s.christ.de/Article/GetArticles";
            
            // Speicherpfade
            string filePath = "articles.csv";
            string accessPath = "access.csv";

            HttpClient httpClient = new();
            var builder = WebApplication.CreateBuilder(args);
            var storageService = new StorageService(filePath, accessPath);

            // Singleton Pattern => Solo-Instanz, globaler Zugriffspunkt
            builder.Services.AddSingleton(storageService);

            // Dependency Injection (DI) => ArticleApiService wird als Singleton registriert,
            // welcher den httpClient und storageService als Abhängigkeit verwendet
            builder.Services.AddSingleton<ArticleApiService>(new ArticleApiService(httpClient, apiUrl,
                storageService));

            // Builder Pattern => Konfiguration der WebApplication vor Initialisierung
            var app = builder.Build();

            // Wechseln für Produktiv / Entwicklung
            if (app.Environment.IsDevelopment())
            {
                // Fehlerbehandlungs-Middleware => leitet Fehler auf Error-Controller um
                app.UseExceptionHandler("/Error");

                // Middleware für HTTP-Strict-Transport-Security => leitet HTTP-Anfragen auf HTTPS um
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.Run();
        }
    }
}