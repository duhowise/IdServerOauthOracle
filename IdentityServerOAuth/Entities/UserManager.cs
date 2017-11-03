using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Entities
{
    public class UserManager : UserManager<IdentityUser, string>
    {
        public UserManager(UserStore userStore)
            : base(userStore)
        {
        }
    }
}