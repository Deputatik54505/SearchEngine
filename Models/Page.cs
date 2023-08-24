using System.Net;
using System.Xml.Linq;

namespace SearchEngine.Models;

public class Page
{
    public Page(string url, string text)
    {
        Url = url;
        Text = text;
        LastUpdate = DateOnly.FromDateTime(DateTime.Now);
    }
    public string Url { get; set; }
    public Site? Site { get; set; }
    public string Text { get; set; }
    public DateOnly LastUpdate { get; set; }
}