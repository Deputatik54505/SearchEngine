using SearchEngine.Models;

namespace SearchEngine.Data.Repositories;

public interface ITokenRepository
{
	public Task<Token?> GetAsync(string type);
	public bool Create(Token token);
	public bool Update(Token token);
	public bool Delete(Token token);
}