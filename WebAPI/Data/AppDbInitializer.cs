using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebAPI.Data.Models;

namespace WebAPI.Data
{
    public class AppDbInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new User()
                    {
                        FirstName = "User",
                        LastName = "2",
                        Email = "user2@gmail.com",
                        Hash = "sdfsdfgdsfgsfwfwsfswfsfsd",
                        CreatedAt = DateTime.Now
                    },
                    new User()
                    {
                        FirstName = "User",
                        LastName = "3",
                        Email = "user3@gmail.com",
                        Hash = "sdfsdrttrtrstfwfwsfswfsfsd",
                        CreatedAt = DateTime.Now
                    },
                    new User()
                    {
                        FirstName = "User",
                        LastName = "1",
                        Email = "user1@gmail.com",
                        Hash = "sdfsdfwfwsfswfsfsd",
                        CreatedAt = DateTime.Now
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
