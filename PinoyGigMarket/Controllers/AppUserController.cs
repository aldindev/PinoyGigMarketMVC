using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;
using System;

namespace PinoyGigMarket.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public AppUserController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                ProfilePicturePath = user.ProfilePicturePath, 
                City = user.City,
                Country = user.Country,
                PostalCode = user.PostalCode,
                AboutMe = user.AboutMe,
                StatusMessage = user.StatusMessage
            };

            return View(model); // This should match the view's expected model type
        }

        public  async Task<IActionResult> MySkills()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var mySkills = await _context.Skills
                .Where(s => s.UserID == user.Id)
                .Select(s => new SkillViewModel
                {
                    SkillId = s.SkillId,
                    SkillName = s.SkillName,
                    Desc = s.Desc,
                    Rate = s.Rate
                })
                .ToListAsync();

            return View(mySkills);
        }

        public IActionResult ShowAddSkillForm()
        {
            return PartialView("_AddSkill", new SkillViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(SkillViewModel skillViewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var skill = new Skill
                {
                    SkillName = skillViewModel.SkillName,
                    Desc = skillViewModel.Desc,
                    Rate = skillViewModel.Rate,
                    UserID = user.Id,
                    User = user
                };

                _context.Skills.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction("MySkills");
            }

            return PartialView("_AddSkill", skillViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || skill.UserID != user.Id)
            {
                return Unauthorized();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MySkills));
        }

        public async Task<IActionResult> EditSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || skill.UserID != user.Id)
            {
                return Unauthorized();
            }

            var skillViewModel = new SkillViewModel
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName,
                Desc = skill.Desc,
                Rate = skill.Rate
            };

            return PartialView("_EditSkill", skillViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSkill(SkillViewModel skillViewModel)
        {
            if (ModelState.IsValid)
            {
                var skill = await _context.Skills.FindAsync(skillViewModel.SkillId);
                if (skill == null)
                {
                    return NotFound();
                }

                var user = await _userManager.GetUserAsync(User);
                if (user == null || skill.UserID != user.Id)
                {
                    return Unauthorized();
                }

                skill.SkillName = skillViewModel.SkillName;
                skill.Desc = skillViewModel.Desc;
                skill.Rate = skillViewModel.Rate;

                _context.Update(skill);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MySkills));
            }

            return PartialView("_EditSkill", skillViewModel);
        }     

        [HttpPost]
        public async Task<IActionResult> UploadProfilePic(ProfileViewModel model)
        {

            

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return NotFound();

                if (model.ProfilePicture != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads", "profilepics");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(fileStream);
                    }

                    user.ProfilePicturePath = "/uploads/profilepics/" + uniqueFileName;
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index"); // Adjust as needed
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            
            }

            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Address = model.Address;
                user.City = model.City;
                user.Country = model.Country;
                user.PostalCode = model.PostalCode;
                user.AboutMe = model.AboutMe;
                user.StatusMessage = model.StatusMessage;
                                
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index","Appuser");
        }


    }
}

