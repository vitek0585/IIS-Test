using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace LogOutUser.Controllers
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class ChatHub : Hub
    {

        static ChatHub()
        {
        }
        public void Subscribe(string groupName)
        {
            Groups.Add(Context.ConnectionId, "subscriptionId");
        }
        public void UpdateItem(Item item)
        {
            //if(Context.User.IsInRole(''))
            Clients.Caller.updateItem(item);
            Clients.Group("subscriptionId").updateItem(item);
        }
        public void AddItem(Item item)
        {
            Clients.Caller.addItem(item);
            Clients.Group("subscriptionId").addItem(item);
        }

        public async Task VeryBigMethod()
        {
            Clients.Caller.start();
            var factory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
            var listTask = new List<Task>();
            SemaphoreSlim _ss = new SemaphoreSlim(1);
            for (int i = 0; i < 10; i++)
            {
                _ss.Wait(TimeSpan.FromMinutes(10));
                var task = Task.Run(async () =>
               {
                   try
                   {
                       var contextC =(MyContext)GlobalHost.DependencyResolver.GetService(typeof(MyContext));
                       var context = contextC.CategoryContext;
                       var set = context.Set<Category>();
                       var aC = set.Attach(new Category() {CategoryName = $"new", RefPhoto = "Foto.img"});
                       context.Entry(aC).State = EntityState.Added;
                       await Task.Delay(100);

                       context.Category.Add(new Category() { CategoryName = $"new" , RefPhoto = "Foto.img" });
                       //(CategoryContext)GlobalHost.DependencyResolver.GetService(typeof(CategoryContext))
                       var c = context.Category.Find(i);
                       if (c != null)
                       {
                           c.CategoryName = c.CategoryName + "?";
                       }
                       await Task.Delay(100);
                       await Task.Yield();
                       await Task.Yield();
                       var cc = context.CategoryConsumer.Find(1);
                       Clients.Caller.onError(context.GetHashCode());

                       cc.CategoryConsumerName = "haha - ";
                       Clients.Caller.onEnd();
                       var contextC1 = ((MyContext)GlobalHost.DependencyResolver.GetService(typeof(MyContext))).CategoryContext;
                       Clients.Caller.onError(contextC1.GetHashCode());

                       await Task.Run(async () =>
                       {
                           var http = new HttpClient();
                           var r = await http.GetAsync("https://www.google.com.ua/webhp?hl=ru");
                           cc = context.CategoryConsumer.Find(1);
                           cc.CategoryConsumerName = (await r.Content.ReadAsStringAsync()).Length.ToString();
                           await contextC1.SaveChangesAsync();


                       });
                       await contextC1.SaveChangesAsync();
                       await Task.Yield();
                       var d = context.Category.Add(new Category() { CategoryName = $"cat!", RefPhoto = "Foto.img" });
                       context.SaveChanges();

                       await Task.Delay(10);
                       await Task.Yield();
                       var cs = context.Category.Find(d.CategoryId);
                       cs.CategoryName = "sdfsdfs";
                       await contextC1.SaveChangesAsync();

                       await Task.Yield();
                       cs = contextC1.Category.Find(d.CategoryId);
                       cs.CategoryName = "12";
                       //if (i == 9999)
                       context.SaveChanges();
                       contextC1.SaveChanges();
                       //await Task.Delay(1000);
                       Clients.Caller.onError("---------------------------------------------------------------");

                   }
                   catch (Exception e)
                   {
                   }
                   //context.Dispose();
                   Clients.Caller.onError("for end");
                   //await Task.Delay(200);
               });
                task.ContinueWith(t => _ss.Release());

                listTask.Add(task);
            }
            await Task.WhenAll(listTask.ToArray()).ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    foreach (var exception in t.Exception.Flatten().InnerExceptions)
                    {
                        Clients.Caller.onError(exception.ToString(), exception.Message);
                    }
                }
            });
            Clients.Caller.onError("method end");

        }


    }
}