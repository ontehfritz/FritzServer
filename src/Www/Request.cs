using System.Text;

namespace Www;

public class Request
{
    public string Verb { get; init; }
    public string Uri { get; init; }
    public string Version { get; init; }
    public Headers Headers { get; init; }
    public string Body { get; init; }

    public Request(string verb, string uri, string version, Headers headers, string body)
    {
        Verb = verb;
        Uri = uri;
        Version = version;
        Headers = headers;
        Body = body;
    }

    public static Request Parse(string request)
    {
        var reader = new StringReader(request);
        var requestLine = reader.ReadLine().Split(' ');
        if (requestLine.Length != 3)
        {
            throw new Exception("Invalid response");
        }

        var verb = requestLine[0];
        var uri = requestLine[1];
        var version = requestLine[2];
        var headers = new Headers();
        string? line;
        while ((line = reader.ReadLine()) != string.Empty)
        {
            var header = line.Split(":");
            headers.Add(new Header(header[0], header[1]));
        }

        var body = new StringBuilder();
        while ((line = reader.ReadLine()) != null)
        {
            body.AppendLine(line);
        }

        return new Request(verb, uri, version, headers, body.ToString());
    }
}