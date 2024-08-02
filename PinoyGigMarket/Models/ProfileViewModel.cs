namespace PinoyGigMarket.Models
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ProfilePicturePath { get; set; } = "/images/faces/defaultprofilepic.png";
        public IFormFile? ProfilePicture { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? AboutMe { get; set; }
        public string? StatusMessage { get; set; } = string.Empty;
    }

}
