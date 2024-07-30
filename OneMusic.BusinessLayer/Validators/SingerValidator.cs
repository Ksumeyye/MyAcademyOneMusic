using FluentValidation;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Validators
{
    public class SingerValidator : AbstractValidator<Singer>
    {
        public SingerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Şarkıcı Adı Boş Bırakılamaz.").MaximumLength(50).WithMessage("En Fazla 50 KArakterli Şarkıcı İsmi Yazabilirsiniz.").MinimumLength(4).WithMessage("En Az 4 Karakter Yazabilirsiniz.");
        RuleFor(x=>x.ImageUrl).NotEmpty().WithMessage("Resim Url değeri boş bırakılamaz");
        }
    }
}
