using Microsoft.AspNetCore.Identity;
using PinoyGigMarket.Models;
using PinoyGigMarket.Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace PinoyGigMarket.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            if (!userManager.Users.Any())
            {
                // Create users
                var users = new List<AppUser>
            {
                new AppUser { UserName = "michael.brown@example.com", Email = "michael.brown@example.com", FirstName = "Michael", LastName = "Brown", Phone = "1234567891", UserType = UserType.Freelancer },
                new AppUser { UserName = "linda.green@example.com", Email = "linda.green@example.com", FirstName = "Linda", LastName = "Green", Phone = "9876543211", UserType = UserType.Client },
                new AppUser { UserName = "david.lee@example.com", Email = "david.lee@example.com", FirstName = "David", LastName = "Lee", Phone = "5555555556", UserType = UserType.Both },
                new AppUser { UserName = "susan.wilson@example.com", Email = "susan.wilson@example.com", FirstName = "Susan", LastName = "Wilson", Phone = "1231231234", UserType = UserType.Freelancer },
                new AppUser { UserName = "james.hall@example.com", Email = "james.hall@example.com", FirstName = "James", LastName = "Hall", Phone = "3213214321", UserType = UserType.Client },
                new AppUser { UserName = "emma.adams@example.com", Email = "emma.adams@example.com", FirstName = "Emma", LastName = "Adams", Phone = "5675675678", UserType = UserType.Both },
                new AppUser { UserName = "oliver.king@example.com", Email = "oliver.king@example.com", FirstName = "Oliver", LastName = "King", Phone = "8768768765", UserType = UserType.Freelancer },
                new AppUser { UserName = "amelia.clark@example.com", Email = "amelia.clark@example.com", FirstName = "Amelia", LastName = "Clark", Phone = "6546546543", UserType = UserType.Client },
                new AppUser { UserName = "liam.mitchell@example.com", Email = "liam.mitchell@example.com", FirstName = "Liam", LastName = "Mitchell", Phone = "7897897890", UserType = UserType.Both },
                new AppUser { UserName = "mia.turner@example.com", Email = "mia.turner@example.com", FirstName = "Mia", LastName = "Turner", Phone = "4564564567", UserType = UserType.Freelancer },
                new AppUser { UserName = "noah.campbell@example.com", Email = "noah.campbell@example.com", FirstName = "Noah", LastName = "Campbell", Phone = "2342342341", UserType = UserType.Client },
                new AppUser { UserName = "sophia.roberts@example.com", Email = "sophia.roberts@example.com", FirstName = "Sophia", LastName = "Roberts", Phone = "4324324322", UserType = UserType.Both },
                new AppUser { UserName = "lucas.thompson@example.com", Email = "lucas.thompson@example.com", FirstName = "Lucas", LastName = "Thompson", Phone = "1233211233", UserType = UserType.Freelancer },
                new AppUser { UserName = "isabella.white@example.com", Email = "isabella.white@example.com", FirstName = "Isabella", LastName = "White", Phone = "3211233214", UserType = UserType.Client },
                new AppUser { UserName = "ethan.harris@example.com", Email = "ethan.harris@example.com", FirstName = "Ethan", LastName = "Harris", Phone = "5674325675", UserType = UserType.Both },
                new AppUser { UserName = "ava.martin@example.com", Email = "ava.martin@example.com", FirstName = "Ava", LastName = "Martin", Phone = "8765438766", UserType = UserType.Freelancer },
                new AppUser { UserName = "logan.jackson@example.com", Email = "logan.jackson@example.com", FirstName = "Logan", LastName = "Jackson", Phone = "6542346547", UserType = UserType.Client },
                new AppUser { UserName = "mia.lopez@example.com", Email = "mia.lopez@example.com", FirstName = "Mia", LastName = "Lopez", Phone = "7898767898", UserType = UserType.Both },
                new AppUser { UserName = "jackson.gonzalez@example.com", Email = "jackson.gonzalez@example.com", FirstName = "Jackson", LastName = "Gonzalez", Phone = "4562344569", UserType = UserType.Freelancer },
                new AppUser { UserName = "olivia.moore@example.com", Email = "olivia.moore@example.com", FirstName = "Olivia", LastName = "Moore", Phone = "2341232340", UserType = UserType.Client }
            };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pass_123");
                }

                // Create projects
                var projects = new List<Project>
            {
                new Project { ClientID = users[1].Id, Title = "Bathroom Renovation", Desc = "Complete bathroom renovation including plumbing and tiling.", Status = ProjectStatus.Open, Budget = 2000.00m },
                new Project { ClientID = users[1].Id, Title = "Electrical Wiring", Desc = "Rewiring of a two-story house.", Status = ProjectStatus.Open, Budget = 3000.00m },
                new Project { ClientID = users[4].Id, Title = "Carpentry Work", Desc = "Build custom kitchen cabinets.", Status = ProjectStatus.Open, Budget = 2500.00m },
                new Project { ClientID = users[7].Id, Title = "Roof Repair", Desc = "Fix leaks and replace damaged shingles.", Status = ProjectStatus.Open, Budget = 1500.00m },
                new Project { ClientID = users[10].Id, Title = "Painting Job", Desc = "Paint the interior of a 3-bedroom house.", Status = ProjectStatus.Open, Budget = 1800.00m },
                new Project { ClientID = users[13].Id, Title = "Flooring Installation", Desc = "Install hardwood flooring in living room and kitchen.", Status = ProjectStatus.Open, Budget = 2200.00m },
                new Project { ClientID = users[16].Id, Title = "Plumbing Fixes", Desc = "Fix leaks in kitchen and bathroom.", Status = ProjectStatus.Open, Budget = 1200.00m },
                new Project { ClientID = users[19].Id, Title = "Garden Shed Construction", Desc = "Construct a wooden garden shed.", Status = ProjectStatus.Open, Budget = 1700.00m }
            };

                await context.Projects.AddRangeAsync(projects);

                // Create proposals
                var proposals = new List<Proposal>
            {
                new Proposal { ProjectID = 1, FreelancerID = users[0].Id, BidAmount = 1900.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 1, FreelancerID = users[2].Id, BidAmount = 1950.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 2, FreelancerID = users[0].Id, BidAmount = 2900.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 2, FreelancerID = users[5].Id, BidAmount = 2950.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 3, FreelancerID = users[6].Id, BidAmount = 2450.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 4, FreelancerID = users[9].Id, BidAmount = 1400.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 5, FreelancerID = users[12].Id, BidAmount = 1750.00m, Status = ProjectStatus.Open },
                new Proposal { ProjectID = 6, FreelancerID = users[15].Id, BidAmount = 2100.00m, Status = ProjectStatus.Open }
            };

                await context.Proposals.AddRangeAsync(proposals);

                // Create skills
                var skills = new List<Skill>
            {
                new Skill { UserID = users[0].Id, SkillName = "Plumbing", Desc = "Expert in residential and commercial plumbing services.", Rate = "$40/hour" },
                new Skill { UserID = users[2].Id, SkillName = "Electrical", Desc = "Licensed electrician with 10 years of experience.", Rate = "$50/hour" },
                new Skill { UserID = users[5].Id, SkillName = "Carpentry", Desc = "Skilled carpenter for custom woodwork and cabinetry.", Rate = "$45/hour" },
                new Skill { UserID = users[6].Id, SkillName = "Roofing", Desc = "Experienced in roof installation and repairs.", Rate = "$55/hour" },
                new Skill { UserID = users[9].Id, SkillName = "Painting", Desc = "Professional painter for interior and exterior jobs.", Rate = "$35/hour" },
                new Skill { UserID = users[12].Id, SkillName = "Flooring", Desc = "Expert in hardwood and laminate flooring installation.", Rate = "$50/hour" },
                new Skill { UserID = users[15].Id, SkillName = "Plumbing", Desc = "Reliable plumbing services for repairs and installations.", Rate = "$40/hour" },
                new Skill { UserID = users[18].Id, SkillName = "Construction", Desc = "General contractor for small to medium-sized projects.", Rate = "$60/hour" }
            };

                await context.Skills.AddRangeAsync(skills);

                await context.SaveChangesAsync();
            }
        }
    }

}