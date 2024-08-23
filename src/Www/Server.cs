using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Www;

public class Server
{
    private TcpListener _listener { get; }
    public Server()
    {
        _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 666));
    }

    public void Start()
    {
        _listener.Start();
       
        String data = null;
        
        while (true)
        {
            Console.Write("Waiting for a connection... ");
            using TcpClient client = _listener.AcceptTcpClient();
            Console.WriteLine("Connected!");

            data = null;
            var stream = client.GetStream();
            // Get a stream object for reading and writing
            Request request = ProcessRequest(stream);
            //Console.WriteLine(request.Verb);

          
            Byte[] msg = System.Text.Encoding.ASCII.GetBytes("HTTP/1.1 200 Success.");
            stream.Write(msg, 0, msg.Length);
        }
    }

    public void Stop()
    {
        _listener.Stop();   
    }

    private Request ProcessRequest(NetworkStream stream)
    {
        byte[] myReadBuffer = new byte[1024];
        StringBuilder message = new StringBuilder();
        int numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
        
        while (numberOfBytesRead > 0)
        {
            message.Append(Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.Write(message.ToString());
            numberOfBytesRead = stream.DataAvailable ? stream.Read(myReadBuffer, 0, myReadBuffer.Length) : 0;
        }
       

        return Request.Parse(message.ToString());
    }
    
    private string Response()
    {
        return String.Empty;
    }
}