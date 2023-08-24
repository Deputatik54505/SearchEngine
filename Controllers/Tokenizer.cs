using SearchEngine.Data.Repositories;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Controllers;

public class Tokenizer
{
	private readonly ITokenRepository _repository;
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	private const string Splitter = " ";

	public Tokenizer(ITokenRepository repository)
	{
		_repository = repository;
	}

	public void Tokenize(Page page)
	{
		var text = page.Text;
		string[] tokens = text.Split(Splitter);

		foreach (var token in tokens)
		{
			
		}
	}
}