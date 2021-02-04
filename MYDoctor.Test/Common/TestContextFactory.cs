using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Test.Common
{
    public class TestContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();
            // add userProfiles
            context.UserProfiles.AddRange(new[] {
                new UserProfile { Id=1,Name="Test",Email="Test@gmail.com"},
                new UserProfile { Id=2,Name="Test2",Email="Test2@gmail.com"}
            });
            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
