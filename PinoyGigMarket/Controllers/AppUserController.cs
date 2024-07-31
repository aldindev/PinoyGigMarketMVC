using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppUserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Handle the case where no users are found
                return View("NoUsers"); // Create a view for this case or handle it differently
            }
            return View(user);
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
    }
}

