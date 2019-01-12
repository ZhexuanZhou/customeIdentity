using System.Collections.Generic;
using System.Threading.Tasks;
using customeIdentity.Database;
using customeIdentity.Models;
using Microsoft.AspNetCore.Mvc;

namespace customeIdentity.Contorllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        private AppDbContext _appDbContext;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        public TestController(AppDbContext appDbContext, UserManager<User> userManager, RoleManager<Role> roleManager) 
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = new List<Role>()
            {
                new Role {Text = "SuperUser", Describtion = "Full permission"},
                new Role {Text = "User", Describtion = "Limited permission"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            await _userManager.CreateAsync(new User{
                UserName = "Zhexuan",
                Email = "email@example.com"
            }, "Zhexuan363100,");
            var user = await _userManager.FindByNameAsync("Zhexuan");
            await _userManager.AddToRoleAsync(user, "SuperUser");
            return Ok("s");
        }
    }
}