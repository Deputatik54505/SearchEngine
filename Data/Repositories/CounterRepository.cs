using Microsoft.EntityFrameworkCore;
using SearchEngine.Models;

namespace SearchEngine.Data.Repositories;

public class CounterRepository
{
	private readonly ApplicationContext _context;
	private readonly DbSet<Counter> _counters;

	public CounterRepository(ApplicationContext context)
	{
		_context = context;
		_counters = context.Counters;
	}

	public Counter Get(int id)
	{
		return _counters
			.Single(c => c.Id.Equals(id));
	}

	public Counter? Get(string type, string url)
	{
		return _counters
			.SingleOrDefault(c => c.Token.Equals(type) && c.Url.Equals(url));
	}

	public List<Counter> OfType(string type)
	{
		return _counters
			.Where(c => c.Token.Equals(type))
			.ToList();
	}


	public void Update(Counter counter)
	{
		_counters.Update(counter);

		_context.SaveChanges();
		_context.Entry(counter).State = EntityState.Detached;
	}

	public void Create(Counter counter)
	{
		_counters.Add(counter);
		_context.SaveChanges();
		_context.Entry(counter).State = EntityState.Detached;
	}
}