using Microsoft.EntityFrameworkCore;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Data.Repositories;

public class TokenRepository : ITokenRepository
{
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	private readonly ApplicationContext _context;

	public TokenRepository(ApplicationContext context)
	{
		_context = context;
	}

	public async Task<Token?> GetAsync(string type)
	{
		_logger.Trace($"searching for token {type}");
		return await _context.Tokens.Include(t => t.Pages).FirstOrDefaultAsync(token => token.Type.Equals(type));
	}

	public bool Create(Token token)
	{
		if (InContext(token))
		{
			_logger.Warn($"token {token} already in context");
			return false;
		}
		_logger.Trace($"adding token {token} in context");
		_context.Tokens.Add(token);
		return Save();
	}

	public bool Update(Token token)
	{
		if (!InContext(token))
		{
			_logger.Warn($"token {token} wasn't found in context");
			return false;
		}

		_context.Tokens.Update(token);
		return Save();
	}

	public bool Delete(Token token)
	{
		if (!InContext(token))
		{
			_logger.Warn($"token {token} wasn't found in context");
			return false;
		}
		_context.Tokens.Remove(token);
		return Save();
	}

	private bool Save()
	{
		return _context.SaveChanges() > 0;
	}

	private bool InContext(Token token)
	{
		return _context.Tokens.FirstOrDefault(t => t.Type.Equals(token.Type)) != null;
	}
}