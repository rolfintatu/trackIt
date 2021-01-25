using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrackIt.UI.AuthModels;

namespace TrackIt.UI.AuthManagers
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }
    }
}
