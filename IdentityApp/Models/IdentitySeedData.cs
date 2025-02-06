using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models {
    public static class IdentitySeedData {
        private const string adminUser = "philopobic";
        private const string adminPassword = "111.Wassap";

        public static async void IdentityTestUser(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();
            if (context.Database.GetAppliedMigrations().Any()) {
                context.Database.Migrate();//Database update
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);
            if (user == null) {
                user = new AppUser {
                    FullName = "Fatih Eker",
                    UserName = adminUser,
                    Email = "fatiheker97@gmail.com",
                    PhoneNumber = "0532 502 5312"
                };
                await userManager.CreateAsync(user, adminPassword);
            }
        }

    }
}