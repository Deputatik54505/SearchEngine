using SearchEngine.Data.Repositories;
using SearchEngine.Models;

namespace SearchEngine.Controllers;

public class Tokenizer
{
	private readonly ITokenRepository _repository;
	//private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	private const string Splitter = " ";

	public Tokenizer(ITokenRepository repository)
	{
		_repository = repository;
	}
	//TODO optimize this thing, if needed 
	public void Tokenize(Page page)
	{
		foreach (var word in page.Text.Split(Splitter))
		{
			Counter? counter;
			var type = _repository.GetAsync(word).Result;
			if (type != null)
			{
				counter = type.Pages.FirstOrDefault(p => p.Url.Equals(page.Url));
				if (counter != null)
				{
					type.Pages.Remove(counter);
					counter.Entries++;
				}
				else
				{
					counter = new Counter(page.Url);
				}
			}
			else
			{
				counter = new Counter(page.Url);
				type = new Token(word);
			}
			type.Pages.Add(counter);
			_repository.Update(type);
		}
	}
}