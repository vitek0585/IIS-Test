﻿using System.Data.Entity;

namespace LogOutUser
{
    public partial class CategoryContext : DbContext
    {
        public CategoryContext()
            : base("name=Category")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryConsumer> CategoryConsumer { get; set; }
        public virtual DbSet<Consumer> Consumer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.RefPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CategoryConsumers)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Consumer>()
                .Property(e => e.ConsumerName)
                .IsUnicode(false);

            modelBuilder.Entity<Consumer>()
                .HasMany(e => e.CategoryConsumers)
                .WithRequired(e => e.Consumer)
                .WillCascadeOnDelete(false);
        }
    }
}
