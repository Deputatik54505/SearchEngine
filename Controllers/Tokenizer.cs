using System.Text;
using Aspose.Html;
using SearchEngine.Data.Repositories;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Controllers;

public class Tokenizer
{
	private readonly ITokenRepository _repository;
	private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
	public const string Splitter = " ";

	public Tokenizer(ITokenRepository repository)
	{
		_repository = repository;
	}

	//TODO optimize this thing, if needed 
	public void Tokenize(Page page)
	{
		foreach (var word in ParseText(page).Split(Splitter))
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

	public static string ParseText(Page page)
	{
		var url = new Url(page.Url);
		var sb = new StringBuilder();

		using var document = new HTMLDocument(page.Html, url);

		var iterator = document.CreateNodeIterator(document, 
			Aspose.Html.Dom.Traversal.Filters.NodeFilter.SHOW_TEXT);

		while (iterator.NextNode() is { } node)
		{
			sb.Append(node.NodeValue);
		}
		return sb.ToString();
	}
}