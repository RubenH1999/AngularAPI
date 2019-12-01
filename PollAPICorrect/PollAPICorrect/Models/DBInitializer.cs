using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class DBInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            context.Users.AddRange(
                new User { UserName = "Parra", Email = "parra@parra.be", Password = "Plop1234" },
                new User { UserName = "Ruben", Email = "parra@parra.de", Password = "Plop" }
                );
            
            context.SaveChanges();
        }
    }
}
