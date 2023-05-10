// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var list = new List<int>() {  1,2, 3 };
        var test = new Test();
        Parallel.ForEach(list,x =>
            {
                test.PrintSometing();
            });
        Console.ReadLine();

        DateTime.Now
    }

    
}


public class Test
{
    /// <summary>
    /// User By UserName Mutex.
    /// </summary>
    private static readonly AsyncLock UserByUserNameMutex = new AsyncLock();

    private int number = 0;

    public async Task PrintSometing()
    {
        // AsyncLock can be locked asynchronously
        using (await UserByUserNameMutex.LockAsync())
        {
            // It's safe to await while the lock is held
            Console.WriteLine(number++);
        }
    }
}
