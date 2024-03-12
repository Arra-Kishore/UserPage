using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPage.Data;
using UserPage.Models;
using UserPage.Models.Entities;

namespace UserPage.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginForm viewModel)
        {
            var User = new User
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Email = viewModel.Email,

            };
            await dbContext.Users.AddAsync(User);
            await dbContext.SaveChangesAsync();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Users = await dbContext.Users.ToListAsync();
            return View(Users);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var User = await dbContext.Users.FindAsync(Id);
            return View(User);
        }
        [HttpPost]

    public async Task<IActionResult>Edit(User viewModel)
        {
            var User = await dbContext.Users.FindAsync(viewModel.Id);
            if(User is not null)
            {
                User.Id = viewModel.Id;
                User.Name = viewModel.Name;
                User.Email = viewModel.Email;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Users");
        }


        [HttpPost]
        public async Task<IActionResult>Delete(User viewModel)
        {
            var User = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id==viewModel.Id);
            if(User is not null)
            {
                dbContext.Users.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Users");
        }




    }
    
    


}
