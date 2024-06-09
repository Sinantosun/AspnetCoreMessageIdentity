using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Data;
using AspnetCoreMessageIdentity.Models.SignalRModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AspnetCoreMessageIdentity.Hubs
{
    public class SignalRMessageHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public SignalRMessageHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetNickName(string nickname)
        {
            var client = ClientSources.userClients.FirstOrDefault(x => x.NameSurname == nickname);
            if (client == null)
            {
                UserClient userClient = new UserClient()
                {
                    ConnectionId = Context.ConnectionId,
                    NameSurname = nickname,
                };


                await Clients.Others.SendAsync("clientJoined", nickname.ToUpper());

                ClientSources.userClients.Add(userClient);
            }


        }

        public async Task GetConnId(string name)
        {
            //burası layoutta bulunan signalR bağlantısının her seferinde client oluşturması sebebiyle userName alanına göre connectionId değerinin yeni değere eşitlenemesi için gereklidir. bu alan olmassa sayfalar arasında geçiş yaptığımzda anlık mesaj geldiğinde connectionId değiştiği için istediğimiz sonucu elde edemiyoruz.
            var user = await _userManager.FindByNameAsync(name);
            var client = ClientSources.userClients.FirstOrDefault(x => x.NameSurname == user.NameSurname);
            client.ConnectionId = Context.ConnectionId;

        }

        public async Task GetActiveClientCount()
        {
            await Clients.All.SendAsync("ReciveActiveClientCount", ClientSources.userClients.Count());
        }

        public async Task SendMessageAsync(string email, string senderUser)
        {
            var user = await _userManager.FindByEmailAsync(email);

            UserClient client = ClientSources.userClients.FirstOrDefault(x => x.NameSurname.ToLower() == user.NameSurname.ToLower());
            await Clients.Client(client.ConnectionId).SendAsync("reciveMessage",  senderUser.ToUpper() + " Size Yeni Bir Mesaj Gönderdi");
        }
    }
}
