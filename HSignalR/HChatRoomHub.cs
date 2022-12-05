using Microsoft.AspNetCore.SignalR;

namespace HSignalR
{
    public class HChatRoomHub:Hub
    {
        public Task PublicSendMessage(string user,string message)
        {
            string connId = this.Context.ConnectionId;
            var msg = $"{connId}:{DateTime.Now}:{message}";
            return Clients.All.SendAsync("PublicMsgReceived",user,msg);
        }
    }
}
