using JobTaskProject.Data;
using Microsoft.AspNetCore.Identity;

namespace JobTaskProject.Models
{
    public class User : IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string? Image { get; set; }

    }
}
