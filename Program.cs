using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using System.Net.Sockets;
using System.Net;
using System.Text;

void SentMessage(string text)
{
    using (Socket listner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
    {
        var remote_endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        listner.Blocking = true;
        listner.Bind(remote_endpoint);
        listner.Listen(1000);
        Console.WriteLine("wait");
        var socket = listner.Accept();
        Console.WriteLine("connected");
        listner.Close();
    }
}

class ServerSend
{
    public static void Server(string text)
    {
        UdpClient client = new UdpClient(1223);
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("Server is waiting for a message");
        while (true)
        {
            byte[] buffer = client.Receive(ref  point);
            string message_str = Encoding.UTF8.GetString(buffer);
            Message? message = Message.JsonToMessage(message_str);
            message.PrintConsole();
        }
    }
}

class Message
{
    public string? Text { get; set; }
    public string Username_from
    {
        get { return Username_from; }
        set
        {
            if(value == null)
                throw new ArgumentNullException("value cannot be null");
            else 
                Username_from = value;
        }
    }
    public string Username_to
    {
        get { return Username_to; }
        set
        {
            if (value == null)
                throw new ArgumentNullException("value cannot be null");
            else
                Username_to = value;
        }
    }
    public DateTime Date_Time { get; set; }

    public string MessageToJson() => JsonSerializer.Serialize(this);
    public static Message? JsonToMessage(string message) => JsonSerializer.Deserialize<Message>(message);
    public void PrintConsole() => Console.WriteLine(ToString());
    public override string ToString() => $"Received a message from {Username_from}\n{Text}\n{Date_Time}";
}