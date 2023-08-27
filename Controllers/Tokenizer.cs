using System.Text;
using System.Text.RegularExpressions;
using Aspose.Html;
using Aspose.Html.Dom;
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
                type.Pages.Add(counter);
                _repository.Update(type);
			}
            else
            {
                counter = new Counter(page.Url);
                type = new Token(word);
                type.Pages.Add(counter);
                _repository.Create(type);
			}
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
		HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
			, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		// Remove any JavaScript  
		HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
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
			sbHTML.ToString(), "<[^>]*>", "");
	}
}