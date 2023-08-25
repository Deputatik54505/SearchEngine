using System.Net;
using System.Xml.Linq;

namespace SearchEngine.Models;

public class Page
{
    public Page(string url, string html)
    {
        Url = url;
        Html = html;
        LastUpdate = DateOnly.FromDateTime(DateTime.Now);
    }
    public string Url { get; set; }
    public Site? Site { get; set; }
    public string Html { get; set; }
    public DateOnly LastUpdate { get; set; }
}