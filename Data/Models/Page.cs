using System.Net;
using System.Xml.Linq;

namespace SearchEngine.Data.Models;

public class Page
{
    public Page(string url, string site, string text)
    {
        Url = url;
        Site = site;
        Text = text;
        LastUpdate = DateOnly.FromDateTime(DateTime.Now);
    }
    public string Url { get; set; }
    public string Site { get; set; }
    public string Text { get; set; }
    public DateOnly LastUpdate { get; set; }
}