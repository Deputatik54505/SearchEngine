using Microsoft.AspNetCore.Mvc;
using SearchEngine.Data.Repositories;

namespace SearchEngine.Controllers;

public class PageController : Controller
{
	private readonly IPageRepository _pageRepository;

	public PageController(IPageRepository pageRepository)
	{
		_pageRepository = pageRepository;
	}

	public IActionResult Index(string url)
	{
		var page = _pageRepository.GetAsync(url).Result;
		return View(page);
	}
}