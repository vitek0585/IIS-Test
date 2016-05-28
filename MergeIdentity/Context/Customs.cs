using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MergeIdentity.Context
{
    public class ApplicationUserRole : IdentityUserRole
    {
       
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationRole : IdentityRole<String, ApplicationUserRole>
    {
        //public ICollection<User> Users { get; set; }
    }

    public class ApplicationUserClaim : IdentityUserClaim { }

    public class ApplicationUserLogin : IdentityUserLogin { }
}