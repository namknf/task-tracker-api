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
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, "123456789");
            builder.HasData(user);

            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "anya.makeeva.2004@mail.ru",
                FirstName = "Ann",
                LastName = "Makeeva",
                Position = "Frontend Developer"
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, "qwerty123");
            builder.HasData(user);
            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "9093264418es@gmail.com",
                FirstName = "Anastasia",
                LastName = "Malkina",
                Position = "Backend Developer"
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, "nastik");
            builder.HasData(user);
        }
    }
}
