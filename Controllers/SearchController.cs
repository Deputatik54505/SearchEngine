using Microsoft.AspNetCore.Mvc;
using SearchEngine.Data.Repositories;
using ILogger = NLog.ILogger;

namespace SearchEngine.Controllers;

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


	public IActionResult Search(string searchString, int onPage = 10)
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

		var pages = rating
			.OrderByDescending(v => v.Value)
			.Take(onPage)
			.Select(v => v.Key)
			.ToList()
			.Select(item => _pageRepository.GetAsync(item).Result)
			.Where(page => page != null)
			.ToList();

		return View(pages);
	}


}