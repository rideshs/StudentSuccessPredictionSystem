using Microsoft.AspNetCore.Identity;

namespace StudentSuccessPrediction.Models
{       
    // change the default User class from IdentityUser to ApplicationUser

    public class ApplicationUser: IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 3;
        public byte[] ProfilePicture { get; set; }=Array.Empty<byte>();
    }
}