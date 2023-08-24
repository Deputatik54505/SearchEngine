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
    /// IEnumerable of Counters with cites includes this type in text
    /// </summary>
    public IEnumerable<Counter>? Pages { get; set; }
}