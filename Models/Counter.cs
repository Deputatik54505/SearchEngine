namespace SearchEngine.Models;
/// <summary>
/// Support model, which helps to make
/// full text search prioritized 
/// </summary>
public class Counter
{
	public Counter(string url)
	{
		Url = url;
	}

	public int Id { get; set; }
	public int Entries { get; set; }
	public string Url { get; set; }
	public Page Page { get; set; }

}