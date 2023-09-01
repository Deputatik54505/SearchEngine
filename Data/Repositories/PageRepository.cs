using Microsoft.EntityFrameworkCore;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Data.Repositories;

public class PageRepository : IPageRepository
{
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	private readonly ApplicationContext _context;
	private readonly DbSet<Page> _pages;

	public PageRepository(ApplicationContext context)
	{
		_context = context;
		_pages = context.Pages;
	}


	public async Task<Page?> GetAsync(string url)
	{
		_logger.Trace($"searching for page {url} in context");
		return await _pages
			.Include(p=>p.Site).FirstOrDefaultAsync(page => page.Url.Equals(url));
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
		_pages.Add(page);
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
		_pages.Update(page);
		var isSuccessful =  _context.SaveChanges() > 0;
		_context.Entry(page).State = EntityState.Detached;
		return isSuccessful;
	}

	public bool Delete(Page page)
	{
		if (!InContext(page))
		{
			_logger.Warn($"no page with url {page.Url} in context");
			return false;
		}

		_logger.Trace($"removing page {page} from context");

		_pages.Remove(page);
		return Save();
	}

	private bool Save()
	{
		return _context.SaveChanges() > 0;
	}

	private bool InContext(Page page)
	{
		return _pages.Contains(page);
	}
}