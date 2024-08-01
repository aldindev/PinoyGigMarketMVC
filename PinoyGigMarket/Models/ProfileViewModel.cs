namespace PinoyGigMarket.Models
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ProfilePicturePath { get; set; } = "/images/faces/defaultprofilepic.png";
        public IFormFile ProfilePicture { get; set; }
    }

}
