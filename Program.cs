int sum1 = new int();
int sum2 = new int();
object? array1 = new int[]{ 1, 15, 18, 3, 89, 4 };
int[] array2 = new int[]{ 2, 14, 64, 118, 29, 5, 0, 7, 44 };

Thread thread1 = new Thread(new ParameterizedThreadStart(Sum1));
Thread thread2 = new Thread(Sum2);

thread1.Start(array1);
thread2.Start();
thread1.Join(1000);
thread2.Join(1000);
Console.WriteLine($"{sum1} + {sum2} = {sum1 + sum2}");
Console.ReadLine();

void Sum1(object? some_obj)
{
    if (some_obj is int[] && some_obj is not null)
    {
        foreach (int i in (int[])some_obj)
            sum1 += i;
    } 
    else
        throw new ArgumentNullException(nameof(some_obj));
}
void Sum2() => sum2 = array2.Sum();
