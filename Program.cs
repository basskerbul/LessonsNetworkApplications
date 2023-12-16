using System.Net;
using System.Net.NetworkInformation;


static async Task Method()
{
    string website = "google.com";
    IPAddress[] addresses = Dns.GetHostAddresses(website);

    Dictionary<IPAddress, long> pings = new Dictionary<IPAddress, long>();
    List<Thread> threads = new List<Thread>();


    foreach (IPAddress address in addresses)
    {
        var thread = Task.Run(async() =>
        {
            Ping ping = new Ping();
            var reply = await ping.SendPingAsync(address);
            pings.Add(address, reply.RoundtripTime);
            Console.WriteLine($"ip: {address} ping: {reply.RoundtripTime}");
        });
        threads.Add(thread);
    }

    Task.WaitAll(threads.ToArray());

    long min_ping = pings.Min(x => x.Value);
    Console.WriteLine(min_ping.ToString());
}
