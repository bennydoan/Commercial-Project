using Microsoft.AspNetCore.Identity;

namespace CommercialShop.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
