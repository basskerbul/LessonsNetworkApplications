using System.Text;

byte[] data1 = Encoding.UTF8.GetBytes("it's data");
int[] data = { 1, 2, 3, 4, 5 };
try
{
    // Асинхронная обработка массива
    Task<int[]> processedDataTask = ProcessArrayAsync(data);

    // Асинхронный вывод результатов на консоль
    Console.WriteLine("\nProcessed Data:");
    int[] processedData = await processedDataTask;
    foreach (var item in processedData)
    {
        Console.Write($"{item} ");
    }

    // Асинхронная операция после обработки массива с использованием ContinueWith
    var sumTask = processedDataTask.ContinueWith(t => ProcessSumAsync(t.Result));
    int sum = await await sumTask;
    Console.WriteLine($"\nSum of Processed Data: {sum}");
}
catch (Exception ex)
{
    Console.WriteLine($"\nError: {ex.Message}");
}
async Task<int> ProcessSumAsync(int[] data)
{
    await Task.Delay(1000);
    return data.Sum();
}
async Task<int> ProcessArrayAsync(int[] data)
{
    return Task.WaitAll(Array.ConvertAll(data, async (item) => await ProcessEventAsync(item)));
}
async Task<int> ProcessEventAsync(int x)
{
    await Task.Delay(1000);
    return x * 2;
}


using (MemoryStream ms = new MemoryStream())
{
    await ProcessMemoryStreamAsync(ms);
}

async Task ProcessMemoryStreamAsync(MemoryStream ms)
{
    byte[] buffer = new byte[1024];
    var bytes_read = 0;
    StringBuilder sb = new StringBuilder();

    while ((bytes_read = await ms.ReadAsync(buffer, 0, buffer.Length)) >= 0)
        sb.Append(Encoding.UTF8.GetString(buffer, 0, bytes_read));

    await Console.Out.WriteLineAsync(sb.ToString());
}