using System.Data.Entity;

namespace LogOutUser.Models.Entity
{
    public partial class CredoLabContext : DbContext
    {
        public CredoLabContext()
            : base("name=CredoLabContext")
        {
        }

        public virtual DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<ScoreCard> ScoreCards { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<TelcoConnectionPhoneCode> TelcoConnectionPhoneCodes { get; set; }
        public virtual DbSet<TelcoConnection> TelcoConnections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Applications)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatorId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.TelcoConnections)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);
        }
    }
}
