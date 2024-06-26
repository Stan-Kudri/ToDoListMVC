﻿using Microsoft.EntityFrameworkCore;
using ToDoList.Core.DBContext.Configurations;
using ToDoList.Core.Models;
using ToDoList.Core.Models.ToDoItem;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ToDoItems> ToDoItems { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfigeration).Assembly);
    }
}
