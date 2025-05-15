using Microsoft.AspNetCore.Mvc;
//namespace Tomal.Models
namespace WorkFinder.Models
{
    public class LoginViewModel 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin", "Worker", or "WorkProvider"
    }
}
