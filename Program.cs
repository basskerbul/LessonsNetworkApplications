using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using System.Net.Sockets;
using System.Net;
using System.Text;


Message message = new Message() { Text = "Wake up", Date_Time = DateTime.UtcNow, Username_from = "Morpheus", Username_to = "Neo"};
string json = message.MessageToJson();
Console.WriteLine(json);
Message? msd_deser =  Message.JsonToMessage(json);

void SentMessage(string text)
{
    using (Socket listner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
    {
        var remote_endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
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
        UdpClient client = new UdpClient();
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("Server is waiting for a message");
        while (true)
        {
            byte[] buffer = client.Receive(ref  point);
            string message_str = Encoding.UTF8.GetString(buffer);
            Message? message = Message.JsonToMessage(message_str);
            Console.WriteLine($"Received a message from {message?.Username_from}\n{message?.Text}\n{message?.Date_Time}");

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

}