using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Validators
{
	public class CustomErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError PasswordTooShort(int length)
		{
			return new IdentityError
			{
				Description = "Şifre En Az 6 Karakterden Oluşmalıdır."
			};
		}
		public override IdentityError PasswordRequiresDigit()
		{
			return new IdentityError
			{
				Description = "Şifre En Az Bir Rakam (1-2-3..) İçermelidir."
			};
		}
		public override IdentityError PasswordRequiresLower()
		{
			return new IdentityError
			{
				Description="Şifre En Az Bir Küçük Harf (a-z) İçermelidir."
			};

		}
		public override IdentityError PasswordRequiresUpper() 
		{
			return new IdentityError
			{
				Description = "Şifre En Az Bir Büyük Harf (A-Z) İçermelidir."
			};
		}
		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError
			{
				Description=" Şifre En Az Bir Özel Karakter (*,-,_,+...) İçermelidir."
			};
		}
	}
}
