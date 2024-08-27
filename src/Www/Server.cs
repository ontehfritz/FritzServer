using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Www;

public class Server
{
    private TcpListener _listener { get; }
    private Config _config; 
    public Server(Config config)
    {
        _config = config;
        _listener = new TcpListener(new IPEndPoint(IPAddress.Any, config.Port));
    }

    public async Task Start()
    {
        _listener.Start();
        
        while (true)
        {
            Console.Write("Waiting for a connection... ");
            using TcpClient client = await _listener.AcceptTcpClientAsync();
            Console.WriteLine("Connected!");
            var stream = client.GetStream();
            var request = await ProcessRequest(stream);
            var response = await ProcessResponse(request);
            
            var msg = Encoding.ASCII.GetBytes(response.Get());
            stream.Write(msg, 0, msg.Length);
        }
    }

    public void Stop()
    {
        _listener.Stop();   
    }

    private async Task<Request> ProcessRequest(NetworkStream stream)
    {
        var myReadBuffer = new byte[1024];
        var message = new StringBuilder();
        var numberOfBytesRead = await stream.ReadAsync(myReadBuffer);
        
        while (numberOfBytesRead > 0)
        {
            message.Append(Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.Write(message.ToString());
            numberOfBytesRead = stream.DataAvailable ? await stream.ReadAsync(myReadBuffer) : 0;
        }
        
        return Request.Parse(message.ToString());
    }
    
    private async Task<Response> ProcessResponse(Request request)
    {
        var headers = new Headers();
        headers.Add("Content-Type", "text/html; charset=utf-8");
        using StreamReader reader = new($"{_config.RootDir}{request.Uri}");
        var body = await reader.ReadToEndAsync();
        
        if (body.Length > 0)
        {
            headers.Add("Content-Length",  Encoding.ASCII.GetByteCount(body).ToString());
        }

        var response = new Response("HTTP/1.1", 200, "Success", headers, body);
        return response;
    }
}