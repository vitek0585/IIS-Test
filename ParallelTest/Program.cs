using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTest
{

    public class Program
    {
        public static int Main(string[] args)
        {
            int result = 0;
            var runnerTasks = Provider.GetRunnerTasks();
            var listCalculation = new List<Task<int>>();
            var continTasks = new List<Task>();
            //while (runnerTasks.Any())
            //{
            //    var t = Task.WhenAny(runnerTasks).Result;
            //    runnerTasks.Remove(t);
            //    var tmp = Contination(t);
            //    listCalculation.Add(tmp);
            //}
            foreach (var runnerTask in runnerTasks)
            {
                runnerTask.ContinueWith((task, obj) => Console.WriteLine("foreach " + task.Exception.InnerExceptions.First().Message), null,
                    TaskContinuationOptions.OnlyOnFaulted);
                var contin = runnerTask.ContinueWith(
                (task, obj) => ((ICollection<Task<int>>)obj).Add(Contination(task)),
                listCalculation, TaskContinuationOptions.NotOnFaulted);

                continTasks.Add(contin);
            }

            try
            {
                Task.WhenAll(runnerTasks).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch runnerTasks");
                continTasks = continTasks.Where(t => !t.IsFaulted).ToList();
            }
            try
            {
                Task.WaitAll(continTasks.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch continTasks");
            }

            Console.WriteLine("All data load " + listCalculation.Count);
            if (listCalculation.Any())
                result = Task.Factory.ContinueWhenAll(listCalculation.ToArray(), (tasks) =>
                 {
                     Console.WriteLine("summary calculate - " + tasks.Length);
                     var sum = 0;
                     foreach (Task<int> task in tasks)
                     {
                         Console.WriteLine("summary foreach");
                         sum += task.Result;
                     }
                     return sum;
                 }).Result;
            Console.WriteLine("end calculate");

            Console.WriteLine($"Result {result}");
            // Console.ReadKey();
            return result;
        }

        public static Task<int> Contination(Task<DataSetTask> dataSeTask)
        {
            return CalculatorSqrc.MathSqrc(dataSeTask.Result.Operation());

        }

    }

    class Provider
    {
        static List<DataSetTask> _items = new List<DataSetTask>()
            {
                new DataSetTask(1),
                new DataSetTask(),
                new DataSetTask(3),
                new DataSetTask(4),
                new DataSetTask(5),
            };

        public static ICollection<Task<DataSetTask>> GetRunnerTasks()
        {
            var i = 1;
            var factory = new TaskFactory();
            var list = new List<Task<DataSetTask>>();
            for (int ii = 0; ii < _items.Count; ii++)
            {
                var cur = ii;
                list.Add(Task.Run(() =>
                {
                    if (_items[cur].Id == 1)
                        throw new Exception("Satrt Into task ");
                    //Thread.Sleep(new Random().Next(200, 2000));
                    Console.WriteLine("Result ready = " + _items[cur].Id);
                    //throw new Exception("Provider exception");

                    return _items[cur];
                }));
            }

            return list;
        }

    }
    class CalculatorSqrc
    {
        public static TaskFactory _factpry = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
        public static Task<int> MathSqrc(int number)
        {
            var contSource = new TaskCompletionSource<int>();

            Task.Run(() =>
            {
                Thread.Sleep(new Random().Next(200, 1000));
                Console.WriteLine("start calculate " + number);
                //return number*number;

                contSource.SetResult(number * number);
            });
            return contSource.Task;
        }

    }
    public class DataSetTask
    {
        private Exception _exception;
        public DataSetTask(int id)
        {
            Id = id;
        }
        public DataSetTask()
        {
            _exception = new Exception("My Exception");
        }
        public int Id { get; set; }

        public int Operation()
        {
            if (_exception != null)
                throw _exception;
            return Id;
        }

    }
}


//var listTasks = new List<Task>();
//            for (int i = 0; i<items.Count(); i++)
//            {
//                var task = factory.StartNew((num) =>
//                {
//                    int score = 0;
//                    int number = (int)num;
//                    Parallel.ForEach(new[] { 1, 2, 1, 2, 1 }, () => (double)0, (current, state, total) =>
//                    {
//                        items[number].Operation(number);
//                        return current + total;
//                    },
//                        (res) =>
//                        {
//                            Console.WriteLine(res);
//                            Interlocked.Add(ref score, Convert.ToInt32(res));
//                        });
//                    Console.WriteLine($"Score {score} ID {number}");
//                    return score;
//                }, i);
//listTasks.Add(task);
//            }
//            factory.ContinueWhenAll(listTasks.ToArray(), tasks =>
//            {
//                Console.WriteLine($"ContinueWhenAll");
//                foreach (Task<int> task in tasks)
//                {
//                    if (task.IsCompleted)
//                    {
//                        result += task.Result;
//                    }
//                    else if (task.IsFaulted)
//                    {
//                        Console.WriteLine(task.Exception.Message);
//                    }
//                }
//            }).Wait();