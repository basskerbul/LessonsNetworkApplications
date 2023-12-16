int x, y;
int[] a_x = { 5, 15, 8, 2, 13 };
int[] a_y = { 32, 1, 8, 54, 72, 3 };

var task1 = Task1(null);
var task2 = Task2(null);
x = await task1;
y = await task2;

Console.WriteLine($"{x} + {y} = {x + y}");
Console.ReadKey();

async Task<int> Task1(object? array) => await Task.Run(() => a_x.Sum());
async Task<int> Task2(object? array) => await Task.Run(() => a_y.Sum());
