﻿using Microsoft.EntityFrameworkCore;

namespace InterceptorFilterDataBinding.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action"},
                new Category { Id = 2, Name = "Fiction" },
                new Category { Id = 3, Name = "History" }
                );
        }
    }
}
