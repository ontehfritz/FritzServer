using System.Text;

namespace Www;

public record Header(string Name, string Value);

public class Headers
{
    private List<Header> _headers = [];
    
    public void Add(string name, string value)
    {
        _headers.Add(new Header(name, value));
    }

    public void Add(Header header)
    {
        _headers.Add(header);
    }

    public string Get()
    {
        var headers = new StringBuilder();
        foreach (var header in _headers)
        {
            headers.AppendLine($"{header.Name}:{header.Value}");
        }

        return headers.ToString();
    }
}