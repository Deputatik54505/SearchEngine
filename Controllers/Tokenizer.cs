using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Data;
using SearchEngine.Data.Repositories;
using SearchEngine.Models;
using ILogger = NLog.ILogger;

namespace SearchEngine.Controllers;

public class Tokenizer
{
	private readonly ApplicationContext _context;
	public static readonly string[] Splitter = { " ", ",", ".", ")", "(" };

	public Tokenizer(ApplicationContext context)
	{
		_context = context;
	}

	public void Tokenize(Page page)
	{
		foreach (var abc in ParseText(page)
			         .Split(Splitter, StringSplitOptions.RemoveEmptyEntries))
		{
			if (string.IsNullOrWhiteSpace(abc) || abc.Any(c => !char.IsLetterOrDigit(c)))
				continue;

			var word = abc.ToLower();

			if (!char.IsLetter(word[^1]))
				word = word.Remove(word.Length - 1, 1);
			if (string.IsNullOrWhiteSpace(word))
				continue;

			var counter = _context.Counters.FirstOrDefault(c=>c.Token.Equals(word) && c.Url.Equals(page.Url));

			if (counter == null)
			{
				counter = new Counter(page.Url, word);
				_context.Add(counter);
				_context.SaveChanges();
			}
			else
			{
				counter.Entries++;
				_context.Counters.Update(counter);
				_context.SaveChanges();
			}

			_context.Entry(counter).State = EntityState.Detached;
		}
	}

	public static string ParseText(Page page)
	{
		string HTMLCode = page.Html;

		// Remove new lines since they are not visible in HTML  
		HTMLCode = HTMLCode.Replace("\n", " ");

		// Remove tab spaces  
		HTMLCode = HTMLCode.Replace("\t", " ");

		// Remove multiple white spaces from HTML  
		HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

		// Remove HEAD tag  
		HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", " "
			, RegexOptions.IgnoreCase | RegexOptions.Singleline);

		// Remove any JavaScript  
		HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", " "
			, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		// Remove any Style sections
		HTMLCode = Regex.Replace(HTMLCode, "<style.*?</style>", " "
			, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		// Replace special characters like &, <, >, " etc.  
		StringBuilder sbHTML = new StringBuilder(HTMLCode);

		// Note: There are many more special characters, these are just  
		// most common. You can add new characters in this arrays if needed  
		string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
			"&gt;", "&reg;", "&copy;", "&bull;", "&trade;","&#39;"};
		string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
		for (int i = 0; i < OldWords.Length; i++)
		{
			sbHTML.Replace(OldWords[i], NewWords[i]);
		}
		// Check if there are line breaks (<br>) or paragraph (<p>)  
		sbHTML.Replace("<br>", "\n<br>");
		sbHTML.Replace("<br ", "\n<br ");
		sbHTML.Replace("<p ", "\n<p ");
		// Finally, remove all HTML tags and return plain text  
		return System.Text.RegularExpressions.Regex.Replace(
			sbHTML.ToString(), "<[^>]*>", " ");
	}
}