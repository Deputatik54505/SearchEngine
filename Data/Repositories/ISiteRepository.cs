using SearchEngine.Models;

namespace SearchEngine.Data.Repositories;

public interface ISiteRepository
{
	public Task<Site?> GetAsync(string url);
	public bool Create(Site site);
	public bool Update(Site site);
	public bool Delete(Site site);
}