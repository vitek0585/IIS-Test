using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTest
{

    class Program
    {
        static void Main(string[] args)
        {
            double result = 0;
            var items = new List<MyClass>()
            {
                new MyClass(1),
                new MyClass(2),
                new MyClass(3),
                new MyClass(4),
                new MyClass(5),
            };
            var factory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
            var listTasks = new List<Task>();
            for (int i = 0; i < items.Count(); i++)
            {
                var task = factory.StartNew((num) =>
                {
                    int score = 0;
                    int number = (int)num;
                    Parallel.ForEach(new[] { 1, 2, 1, 2, 1 }, () => (double)0, (current, state,total) =>
                        {
                            //Console.WriteLine($"{number}) res {res}");
                            //Thread.Sleep(500);
                            items[number].Operation(number);
                            //if (current % 2 == 0)
                                return current + total;

                            return total;
                        },
                        (res) =>
                        {
                            Console.WriteLine(res);
                            Interlocked.Add(ref score, Convert.ToInt32(res));
                        });
                    Console.WriteLine($"Score {score} ID {number}");
                    return score;
                }, i);
                listTasks.Add(task);
            }
            factory.ContinueWhenAll(listTasks.ToArray(), tasks =>
            {
                Console.WriteLine($"ContinueWhenAll");
                foreach (Task<int> task in tasks)
                {
                    if (task.IsCompleted)
                    {
                        result += task.Result;
                    }
                    else if (task.IsFaulted)
                    {
                        Console.WriteLine(task.Exception.Message);
                    }
                }
            }).Wait();
            Console.WriteLine($"Result {result}");
            Console.ReadKey();
        }
    }

    class MyClass
    {
        public MyClass(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public int Operation(int number)
        {
            //Thread.Sleep(500);
            //Console.WriteLine($"{number}) ID {Id}");
            return Id;
        }

    }
}
