using System.Collections.Generic;
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


    }
}