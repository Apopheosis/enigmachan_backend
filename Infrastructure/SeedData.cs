using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmachan.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new ThreadContext(serviceProvider.GetRequiredService<DbContextOptions<ThreadContext>>()))
            {
                if (!context.Threads.Any())
                {
                }

                context.SaveChanges();
            }
        }
        
    }
}