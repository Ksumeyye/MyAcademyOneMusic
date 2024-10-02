using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultTopSingersComponent: ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        public _DefaultTopSingersComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            var topArtists = artists
                            .OrderByDescending(x => x.Id)
                            .Take(9)
                            .ToList();
            return View(topArtists);
        }
    }
}
