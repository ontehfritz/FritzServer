using System.Text;

namespace Www;

public class Response
{
    public string Version { get; init; }
    public int Status { get; init; }
    public string Description { get; init; }
    public Headers Headers { get; init; }
    public string Body { get; init; }

    public Response(string version, int status, string description, Headers headers, string body)
    {
        Version = version;
        Status = status;
        Description = description;
        Headers = headers;
        Body = body;
    }

    public string Get()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{Version} {Status} {Description}");
        builder.Append(Headers.Get());
        builder.AppendLine(string.Empty);
        builder.Append(Body);
        return builder.ToString();
    }
}