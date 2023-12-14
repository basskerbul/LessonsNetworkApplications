using System.Net;
using System.Net.NetworkInformation;

const string website = "google.com";
IPAddress[] addresses = Dns.GetHostAddresses(website);

Dictionary<IPAddress, long> pings = new Dictionary<IPAddress, long>();
List<Thread> threads = new List<Thread>();

foreach (IPAddress address in addresses)
{
    Thread thread = new Thread(() =>
    {
        Ping ping = new Ping();
        PingReply reply = ping.Send(address);
        pings.Add(address, reply.RoundtripTime);
        Console.WriteLine($"ip: {address} ping: {reply.RoundtripTime}");
    });
    threads.Add(thread);
    thread.Start();
}
foreach (var thread in threads)
    thread.Join();

long min_ping = pings.Min(x => x.Value);
Console.WriteLine(min_ping.ToString());