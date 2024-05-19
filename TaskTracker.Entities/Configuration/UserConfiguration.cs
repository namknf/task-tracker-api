using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public void Configure(EntityTypeBuilder<User> builder)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "life_paradishe@mail.ru",
                FirstName = "Ruslan",
                LastName = "Palytin",
                Position = "IOS Developer",
                UserName = "tatarin"
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, "12345qwertY");
            builder.HasData(user);

            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "9093264418es@gmail.com",
                FirstName = "Anastasia",
                LastName = "Malkina",
                Position = "Backend Developer",
                UserName = "nastik"
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, "nastik");
            builder.HasData(user);
        }
    }
}
