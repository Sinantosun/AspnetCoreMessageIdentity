using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Data;
using AspnetCoreMessageIdentity.Models.SignalRModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace AspnetCoreMessageIdentity.Hubs
{
    public class SignalRMessageHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public SignalRMessageHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetConnectionId()
        {
            await Clients.All.SendAsync("connectionIdParamter", Context.ConnectionId);
        }

        public async Task GetNickName(string nickname)
        {
            UserClient userClient = new UserClient()
            {
                ConnectionId = Context.ConnectionId,
                NameSurname = nickname,
            };

            await Clients.Others.SendAsync("clientJoined", nickname);

            ClientSources.userClients.Add(userClient);
        }

        public async Task SendMessageAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            UserClient client = ClientSources.userClients.FirstOrDefault(x => x.NameSurname.ToLower() == user.NameSurname.ToLower());
            await Clients.Client(client.ConnectionId).SendAsync("reciveMessage", "Yeni Mesajınız Var");
        }
    }
}
