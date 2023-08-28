using Microsoft.EntityFrameworkCore;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Data.Repositories;

public class PageRepository : IPageRepository
{
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	private readonly ApplicationContext _context;

	public PageRepository(ApplicationContext context)
	{
		_context = context;
	}


	public async Task<Page?> GetAsync(string url)
	{
		_logger.Trace($"searching for page {url} in context");
		return await _context.Pages.Include(p => p.Site).FirstOrDefaultAsync(page => page.Url.Equals(url));
	}

	public bool Create(Page page)
	{
		if (InContext(page))
		{
			_logger.Warn($"page with url {page.Url} already in context");
			return false;
		}

		page.LastUpdate = DateOnly.FromDateTime(DateTime.Now);
		_logger.Trace($"adding page {page} in context");
		_context.Pages.Add(page);
		return Save();
	}

	public bool Update(Page page)
	{
		if (!InContext(page))
		{
			_logger.Warn($"no page with url {page.Url} in context");
			return false;
		}

		page.LastUpdate = DateOnly.FromDateTime(DateTime.Now);
		_logger.Trace($"updating page {page}");
		_context.Pages.Update(page);
		return Save();
	}

	public bool Delete(Page page)
	{
		if (!InContext(page))
		{
			_logger.Warn($"no page with url {page.Url} in context");
			return false;
		}

		_logger.Trace($"removing page {page} from context");
		_context.Pages.Remove(page);
		return Save();
	}

	private bool Save()
	{
		return _context.SaveChanges() > 0;
	}

	private bool InContext(Page page)
	{
		return _context.Pages.FirstOrDefault(p => p.Url.Equals(page.Url)) != null;
	}
}