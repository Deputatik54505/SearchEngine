using Microsoft.AspNetCore.Mvc;
using SearchEngine.Data.Repositories;
using ILogger = NLog.ILogger;

namespace SearchEngine.Controllers;

public class SearchController : Controller
{
	private readonly IPageRepository _pageRepository;
	private readonly CounterRepository _counterRepository;
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

	public SearchController(IPageRepository pageRepository, CounterRepository counterRepository)
	{
		_pageRepository = pageRepository;
		_counterRepository = counterRepository;
	}


	public IActionResult Search(string searchString)
	{
		var tokens = searchString.Split(Tokenizer.Splitter, StringSplitOptions.RemoveEmptyEntries);

		var rating = new Dictionary<string, int>();

		foreach (var token in tokens)
		{
			var savedToken = _counterRepository.OfType(token);
			if (savedToken.Count==0) continue;
			var tir = savedToken
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
			.Select(v => v.Key)
			.ToList()
			.Select(item => _pageRepository.GetAsync(item).Result)
			.Where(page => page != null)
			.ToList();

		return View(pages);
	}


}