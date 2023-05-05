using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using Microsoft.AspNetCore.Session;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Hubs
{
    public class SingleChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public SingleChatHub(ApplicationDbContext context) { 
            _context = context;
        }  

        public async Task ChangeUserState (string userId, bool activity)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == Convert.ToInt32(userId));
            if (user != null)
            {
                user.isOnline = activity;
               await _context.SaveChangesAsync();
            }
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext().Request.Query["userId"];
            Console.WriteLine("Connected");
            Console.WriteLine(userId + " key and value " + Context.ConnectionId);

            UserSession user_session_info = await _context.UserSession.FirstOrDefaultAsync(u => u.UserId == Convert.ToInt32(userId));

            if(user_session_info != null)
            {
                _context.UserSession.Remove(user_session_info);
            }

            UserSession user = new()
            {
                UserId = Convert.ToInt32(userId),
                ConnectionString = Context.ConnectionId
            };

            await ChangeUserState(userId, true);

            await _context.UserSession.AddAsync(user);
            await _context.SaveChangesAsync();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userId = Context.GetHttpContext().Request.Query["userId"];
            
            Console.WriteLine("Disconnected" + userId);


            UserSession user =  await _context.UserSession.FirstOrDefaultAsync(u => u.UserId == Convert.ToInt32(userId));
            _context.UserSession.Remove(user);
            await _context.SaveChangesAsync();
            await ChangeUserState(userId, false);

            await base.OnDisconnectedAsync(exception);
        }


        [HubMethodName("SendMessage")]
        public async Task SendMessage(string receiverUserId, string message)
        {

            UserSession user = await _context.UserSession.FirstOrDefaultAsync(u => u.UserId == Convert.ToInt32(receiverUserId));

            if(user != null)
            {
                Console.WriteLine(user.ConnectionString);
                await Clients.Client(user.ConnectionString).SendAsync("ReceiveMessage", message, receiverUserId);
            }
            else
            {
                Console.WriteLine("The other end is not in the socket right now");
                // Receiver is not currently online, save the message in the database for future retrieval
                // Save the senderUserId, receiverUserId, and message to the database
            }
        }
    }
}
