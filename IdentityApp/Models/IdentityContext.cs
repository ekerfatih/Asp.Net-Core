using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models {
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<AppUser, AppRole, string>(options) {
        
    }
}