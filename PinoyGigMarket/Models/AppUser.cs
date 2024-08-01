using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PinoyGigMarket.Models
{
    public enum UserType
    {
        Freelancer,
        Client,
        Both
    }
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public string ProfilePicturePath { get; set; } = "/images/faces/defaultprofilepic.png";



        //Navigation properties
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }

    }
}
