using System.Text;

namespace Www;

public class Response
{
    private Headers _headers;
    private string _body;

    public Response(Headers headers, string body)
    {
        _headers = headers;
        _body = body;
    }

    public string Get()
    {
        var builder = new StringBuilder();
        builder.Append(_headers.Get());
        builder.AppendLine(string.Empty);
        builder.Append(_body);
        return builder.ToString();
    }
}