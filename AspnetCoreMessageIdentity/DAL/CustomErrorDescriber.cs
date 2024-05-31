using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMessageIdentity.DAL
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = "PasswordTooShort", Description = "Şifre en az 6 karakterden oluşmalıdır." };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = "PasswordTooShort", Description = "Şifre en az bir rakam içermelidir." };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = "PasswordRequiresLower", Description = "Şifre en az bir küçük harf içermelidir." };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = "PasswordRequiresUpper", Description = "Şifre en az bir büyük harf içermelidir." };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { Code = "PasswordRequiresNonAlphanumeric", Description = "Şifre en az bir özel karakter içermelidir." };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "DuplicateEmail", Description = "Seçtiğiniz e posta kullanılıyor." };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DuplicateUserName", Description = "Seçtiğiniz kullanıcı adı kullanılıyor." };
        }
    }
}
