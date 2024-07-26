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
                    new AppUser
                    {
                        UserName = "john.doe@example.com",
                        Email = "john.doe@example.com",
                        FirstName = "John",
                        LastName = "Doe",
                        Phone = "1234567890",
                        UserType = UserType.Freelancer
                    },
                    new AppUser
                    {
                        UserName = "jane.smith@example.com",
                        Email = "jane.smith@example.com",
                        FirstName = "Jane",
                        LastName = "Smith",
                        Phone = "9876543210",
                        UserType = UserType.Client
                    },
                    new AppUser
                    {
                        UserName = "bob.johnson@example.com",
                        Email = "bob.johnson@example.com",
                        FirstName = "Bob",
                        LastName = "Johnson",
                        Phone = "5555555555",
                        UserType = UserType.Both
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Password123!");
                }

                // Create projects
                var projects = new List<Project>
                {
                    new Project
                    {
                        ClientID = users[1].Id,
                        Title = "E-commerce Website Development",
                        Desc = "Develop a full-featured e-commerce website using ASP.NET Core",
                        Status = ProjectStatus.Open,
                        Budget = 5000.00m
                    },
                    new Project
                    {
                        ClientID = users[1].Id,
                        Title = "Mobile App for Fitness Tracking",
                        Desc = "Create a mobile app for iOS and Android to track fitness activities",
                        Status = ProjectStatus.Open,
                        Budget = 7500.00m
                    }
                };

                await context.Projects.AddRangeAsync(projects);

                // Create proposals
                var proposals = new List<Proposal>
                {
                    new Proposal
                    {
                        ProjectID = 1,
                        FreelancerID = users[0].Id,
                        BidAmount = 4500.00m,
                        Status = ProjectStatus.Open
                    },
                    new Proposal
                    {
                        ProjectID = 1,
                        FreelancerID = users[2].Id,
                        BidAmount = 4800.00m,
                        Status = ProjectStatus.Open
                    },
                    new Proposal
                    {
                        ProjectID = 2,
                        FreelancerID = users[0].Id,
                        BidAmount = 7000.00m,
                        Status = ProjectStatus.Open
                    }
                };

                await context.Proposals.AddRangeAsync(proposals);

                // Create skills
                var skills = new List<Skill>
                {
                    new Skill
                    {
                        UserID = users[0].Id,
                        SkillName = "ASP.NET Core",
                        Desc = "Experienced in developing web applications using ASP.NET Core",
                        Rate = "$50/hour"
                    },
                    new Skill
                    {
                        UserID = users[0].Id,
                        SkillName = "React Native",
                        Desc = "Proficient in building cross-platform mobile apps with React Native",
                        Rate = "$60/hour"
                    },
                    new Skill
                    {
                        UserID = users[2].Id,
                        SkillName = "Python",
                        Desc = "Expert in Python programming and data analysis",
                        Rate = "$55/hour"
                    }
                };

                await context.Skills.AddRangeAsync(skills);

                await context.SaveChangesAsync();
            }
        }
    }
}