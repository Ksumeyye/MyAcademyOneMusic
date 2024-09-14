using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{
	[Authorize(Roles = "Admin")]

	public class RoleAssignController : Controller
	{
		private readonly RoleManager<AppRole> _roleManager;  //Identityden gelen hazır servis
		private readonly UserManager<AppUser> _userManager;  //Identityden gelen hazır servis

		public RoleAssignController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			var values = _userManager.Users.ToList();
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> AssignRole(int id)
		{
			var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
			TempData["userid"] = user.Id;
			var roles = _roleManager.Roles.ToList();
			var userRoles = await _userManager.GetRolesAsync(user);  // userrole'e liste türünde kullanıcının rollerini aldık

			List<RoleAssignViewModel> roleAssignList = new List<RoleAssignViewModel>();

			foreach (var item in roles)
			{
				var model = new RoleAssignViewModel();
				model.RoleId = item.Id;
				model.RoleName = item.Name;
				model.RoleExist = userRoles.Contains(item.Name);
				roleAssignList.Add(model);
			}
			return View(roleAssignList);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model) // kullanıcının birden fazla rolü olabilir o nedenle liste türünde döncek
		{
			int userid = (int)TempData["userid"];

			var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);

			foreach (var item in model)
			{
				if (item.RoleExist)  // rol varsa, checkbox'a tıklayınca ordan anlıcak var mı yok mu
				{
					await _userManager.AddToRoleAsync(user, item.RoleName);
				}
				else  // checkbox'ı boş bırakmışsak onu ordan kaldırcak.
				{
					await _userManager.RemoveFromRoleAsync(user, item.RoleName);
				}
			}
			return RedirectToAction("Index");
		}
	}
}