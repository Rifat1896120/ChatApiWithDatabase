using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(string username, Model.friendChatModel message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);

        }



        public async Task accpetNewFriendSend(string sender, string receiver)
        {
            await Clients.All.SendAsync("accpetNewFriendReceiver", sender, receiver);

        }

        public async Task FriendSendMessageSend(string sender, string receiver, string message, string time)
        {
            await Clients.All.SendAsync("FriendSendMessageReceiver", sender, receiver, message, time);

        }


        public async Task SendCall_1(string sender, string receiver)
        {
            await Clients.All.SendAsync("ReceiveCall_1", sender, receiver);
        }
        public async Task SendCall_2(string sender, string receiver, string message)
        {
            await Clients.All.SendAsync("ReceiveCall_2", sender, receiver, message);
        }
        public async Task Shareip_1(string sender, string receiver, string Id)
        {
            await Clients.All.SendAsync("receiveip_1", sender, receiver, Id);
        }
        public async Task Shareip_2(string sender, string receiver, string Id)
        {
            await Clients.All.SendAsync("receiveip_2", sender, receiver, Id);
        }
        public async Task isrunvideopage(string sender, string receiver, string mess)
        {
            await Clients.All.SendAsync("isrunvideopagerecip", sender, receiver, mess);
        }
    }
}
