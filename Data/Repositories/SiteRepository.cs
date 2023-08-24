using Microsoft.EntityFrameworkCore;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Data.Repositories;

public class SiteRepository : ISiteRepository
{
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	private readonly ApplicationContext _context;

	public SiteRepository(ApplicationContext context)
	{
		_context = context;
	}

	public async Task<Site?> GetAsync(string url)
	{
		_logger.Trace($"searchin for site {url} in context");
		return await _context.Sites.FirstOrDefaultAsync(site => site.Url.Equals(url));
	}

	public bool Create(Site site)
	{
		if (InContext(site))
		{
			_logger.Warn($"site {site} already in context");
			return false;
		}

		_context.Sites.Add(site);
		return Save();
	}

	public bool Update(Site site)
	{
		if (!InContext(site))
		{
			_logger.Warn($"No site {site.Url} in context");
			return false;
		}

		_context.Sites.Update(site);
		return Save();
	}

	public bool Delete(Site site)
	{
		if (!InContext(site))
		{
			_logger.Warn($"No site {site.Url} in context");
			return false;
		}

		_context.Sites.Remove(site);
		return Save();
	}


	private bool Save()
	{
		return _context.SaveChanges() > 0;
	}

	private bool InContext(Site site)
	{
		return _context.Sites.FirstOrDefault(s => s.Url.Equals(site.Url)) != null;
	}
}