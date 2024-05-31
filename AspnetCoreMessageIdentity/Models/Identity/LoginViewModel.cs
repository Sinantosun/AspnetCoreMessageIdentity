using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMessageIdentity.Models.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı bilgisi gerklidir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre bilgisi gerklidir")]
        public string Pwd { get; set; }
    }
}
