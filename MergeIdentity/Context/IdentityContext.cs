using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MergeIdentity.Context
{
    public partial class IdentityContext : IdentityDbContext<User, ApplicationRole, String, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public IdentityContext()
            : base("name=IdentityContext")
        {
        }

        public virtual DbSet<ApplicationCalculationData> ApplicationCalculationDatas { get; set; }
        public virtual DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<PhoneCode> PhoneCodes { get; set; }
        public virtual DbSet<ScoreCard> ScoreCards { get; set; }
        public virtual DbSet<ScoringResult> ScoringResults { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<TelcoConnection> TelcoConnections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(l => l.UserId);

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims");


            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            //modelBuilder.Entity<User>().HasMany(r => r.Roles).WithMany(r => r.);



            //modelBuilder.Entity<Role>().ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationRole>().HasMany(i => i.Users).WithRequired().HasForeignKey(i => i.RoleId);
            //.HasMany(e => e.Users)
            //.WithMany(e => e.Roles)
            //.Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));


            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.AspNetUserClaims)
            //    .WithRequired(e => e.User)
            //    .HasForeignKey(e => e.UserId);
            //modelBuilder.Entity<ApplicationUserRole>().Property(c => c.RoleId).HasColumnName("RoleId");
            //modelBuilder.Entity<ApplicationUserRole>().Property(c => c.UserId).HasColumnName("UserId");

            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { r.UserId, r.RoleId })
            .ToTable("AspNetUserRoles");

            modelBuilder.Entity<ApplicationUserRole>().HasRequired(f => f.Role).WithMany(a => a.Users)
            .HasForeignKey(key => key.RoleId);
            //modelBuilder.Entity<ApplicationRole>()
            //   .HasMany(e => e.Users)
            //   .WithMany(e => e.Role)
            //   .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<ApplicationUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");

            modelBuilder.Entity<User>()
                .HasMany(e => e.Applications)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatorId);

            modelBuilder.Entity<User>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>()
     .HasOptional(e => e.Subscription)
     .WithMany(e => e.AspNetUsers).HasForeignKey(f => f.SubscriptionId);
            //modelBuilder.Entity<User>().HasMany(c => c.Roles1).WithMany(c => c.Users);
            //modelBuilder.Entity<Role>()
            //   .HasMany(e => e.AspNetUsers)
            //   .WithMany(e => e.AspNetRoles)
            //   .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.AspNetUserLogins)
            //    .WithRequired(e => e.User)
            //    .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.TelcoConnections)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscription>()
                .HasMany(e => e.AspNetUsers)
                .WithOptional(e => e.Subscription)
                .HasForeignKey(i => i.SubscriptionId)
                .WillCascadeOnDelete();
            //base.OnModelCreating(modelBuilder);

        }
        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}
