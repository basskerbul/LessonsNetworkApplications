using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LessonsNetworkApplications
{
    public enum Commands
    {
        Register,
        Delete
    }
    public class Message
    {
        public Commands Command { get; set; }
        public string? Text { get; set; }
        public DateTime time { get; set; }
        public string? Username_from { get; set;}
        public string? Username_to { get; set; }

        public string SerializerMessageToJSON() => JsonSerializer.Serialize(this);
        public static Message? DeserializerFromJSON(string message) => JsonSerializer.Deserialize<Message>(message);
        public void PrintConsole() => Console.WriteLine(ToString());
        public override string ToString() => $"{this.time}\nReceived a message from {this.Username_from}";
    }
}
