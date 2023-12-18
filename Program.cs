using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Text.Json;

var vs = Task.Run(() => ServerSend.Server());
Task.WaitAll(vs);


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
    public static void Server()
    {
        UdpClient client = new UdpClient(1223);
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("I listen");
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;

        while (true)
        {
            byte[] buffer = client.Receive(ref point);
            string message_str = Encoding.UTF8.GetString(buffer);
            Task.Run(() =>
            {
                Message? message = Message.JsonToMessage(message_str);
                message?.PrintConsole();
                byte[] reply = Encoding.UTF8.GetBytes("message has been received");
                client.Send(reply);
            }, token);
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
            if (value == null)
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