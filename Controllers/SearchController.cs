using Microsoft.AspNetCore.Mvc;
using SearchEngine.Data.Repositories;
using ILogger = NLog.ILogger;

namespace SearchEngine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : Controller
{
	private readonly IPageRepository _pageRepository;
	private readonly ITokenRepository _tokenRepository;
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

	public SearchController(IPageRepository pageRepository, ITokenRepository tokenRepository)
	{
		_pageRepository = pageRepository;
		_tokenRepository = tokenRepository;
	}


	[HttpGet]
	public IActionResult Search(string searchString, int onPage)
	{
		var tokens = searchString.Split(Tokenizer.Splitter);

		var rating = new Dictionary<string, int>();

		foreach (var token in tokens)
		{
			var savedToken = _tokenRepository.GetAsync(token).Result;
			if (savedToken == null) continue;
			var tir = savedToken.Pages
				.OrderByDescending(c => c.Entries);

			foreach (var counter in tir)
			{
				if (rating.ContainsKey(counter.Url))
					rating[counter.Url] += counter.Entries;
				else
				{
					rating.Add(counter.Url, counter.Entries);
				}
			}
		}

		var list = rating.OrderByDescending(v => v.Value).Take(onPage).ToList();


		return Ok(list);
	}
	
}