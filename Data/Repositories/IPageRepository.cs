using SearchEngine.Models;

namespace SearchEngine.Data.Repositories;

public interface IPageRepository
{
	public Task<Page?> GetAsync(string url);
	public bool Create(Page page);
	public bool Update(Page page);
	public bool Delete(Page page);

}