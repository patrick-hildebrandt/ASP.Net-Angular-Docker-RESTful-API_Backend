using Microsoft.AspNetCore.Mvc;

namespace ChristCodingChallengeBackend.Controllers
{
    [ApiController]
    [Route("api/articles")]
    // primärer Konstruktor
    public class ArticleController(StorageService storageService) : ControllerBase
    {
        private readonly StorageService _storageService = storageService;

        [HttpGet]
        public ActionResult<IEnumerable<JsonArticle>> GetArticles()
        {
            var articles = _storageService.GetArticlesJson();
            return Ok(articles);
        }
    }
}
