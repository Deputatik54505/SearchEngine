namespace SearchEngine.Models;
/// <summary>
/// Model of token, used for fulltext search.
/// </summary>
public class Token
{
	public Token(string type)
	{
		Type = type;
	}
	/// <summary>
	/// The string of token
	/// </summary>
	public string Type { get; set; }
	/// <summary>
	/// Queue of urls with cites includes this type in text
	///  the int number represent count of token entries in text
	/// </summary>
	public PriorityQueue<int,string> Urls { get; set; } = new();
}