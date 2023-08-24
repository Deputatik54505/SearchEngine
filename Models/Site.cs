namespace SearchEngine.Models;

/// <summary>
/// Model of website.
/// </summary>
public class Site
{

    public Site(string ipAddress, string name, string url)
    {
        IpAddress = ipAddress;
        Name = name;
        Url = url;
        LastUpdate = DateOnly.FromDateTime(DateTime.Now);
    }

    /// <summary>
    /// Actual IP address of website
    /// </summary>
    public string IpAddress { get; set; }

    /// <summary>
    /// Name of website for search engine
    /// </summary>
    public string Name { get; set; }

    public string Url { get; set; }

    public DateOnly LastUpdate { get; set; }
}