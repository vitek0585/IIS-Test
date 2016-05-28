using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MergeIdentity.Context
{
    public partial class User : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
        
            Applications = new HashSet<Application>();
            //AspNetUserClaims = new HashSet<AspNetUserClaim>();
            //AspNetUserLogins = new HashSet<AspNetUserLogin>();
            //Roles = new HashSet<IdentityRole>();
        }

        //public string Id { get; set; }

        //[StringLength(256)]
        //public string Email { get; set; }

        //public bool EmailConfirmed { get; set; }

        //public string PasswordHash { get; set; }

        //public string SecurityStamp { get; set; }

        //public string PhoneNumber { get; set; }

        //public bool PhoneNumberConfirmed { get; set; }

        //public bool TwoFactorEnabled { get; set; }

        //public DateTime? LockoutEndDateUtc { get; set; }

        //public bool LockoutEnabled { get; set; }

        //public int AccessFailedCount { get; set; }

        //[Required]
        //[StringLength(256)]
        //public string UserName { get; set; }

        public int? SubscriptionId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmployeeId { get; set; }

        public bool IsActive { get; set; }

        public DateTime WorkStartDate { get; set; }

        public string JobPositionTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Applications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual Subscription Subscription { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<IdentityRole> Roles1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Role> AspNetRoles { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
