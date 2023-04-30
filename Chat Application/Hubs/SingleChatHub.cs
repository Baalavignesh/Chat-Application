using Microsoft.AspNetCore.SignalR;

namespace Chat_Application.Hubs
{
    public class SingleChatHub : Hub
    {
        [HubMethodName("SendMessage")]
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine(user);
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        [HubMethodName("SendOneMessage")]
        public async Task SendOneMessage(string user, string message)
        {
            Console.WriteLine(user);
            Console.WriteLine(message);
            await Clients.User("1").SendAsync("ReceiveMessage", user, message);
        }

    }
}
