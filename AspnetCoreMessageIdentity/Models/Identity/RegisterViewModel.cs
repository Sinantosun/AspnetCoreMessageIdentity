using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMessageIdentity.Models.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Ad soyad bilgisi gereklidir")]
        public string NameSurname { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı bilgisi gereklidir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mail bilgisi gereklidir")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Şifrenizi giriniz")]
        public string Pwd { get; set; }
        [Required(ErrorMessage = "Şifrenizi yeniden giriniz.")]
        [Compare("Pwd",ErrorMessage ="Şifreler eşlenmiyor")]   
        public string ConfirmPwd { get; set; }

        
        public bool IsPolicyRead { get; set; }
    }
}
