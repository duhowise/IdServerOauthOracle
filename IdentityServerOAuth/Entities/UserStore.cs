using IdentityServerOAuth.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Entities
{
    public class UserStore : UserStore<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public UserStore(Context context) : base(context) { }
    }
}