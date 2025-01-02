using Microsoft.AspNetCore.Identity;

namespace LojaRoupas.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? ClienteId { get; set; }
    }
}
