using Microsoft.EntityFrameworkCore;
using SomeWebShowroom.MVC.Data;
using SomeWebShowroom.MVC.Models;
using SomeWebShowroom.MVC.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SomeWebShowroomDbContext context;
        public UserService(SomeWebShowroomDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserDetailRequestModel>> GetAll()
        {
            return this.context.Users.Select(x => new UserDetailRequestModel
            {
                Id = x.Id,
                Username = x.UserName,
                Email = x.Email
            });
        }

        public async Task<UserDetailRequestModel> GetUser(string id)
        {
            return await this.context
                .Users
                .Where(u => u.Id == id)
                .Select(x => new UserDetailRequestModel
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Email = x.Email
                }).FirstOrDefaultAsync();
        }

        public async Task Delete(string id)
        {
            var user = await this.context.Users.FindAsync(id);
            this.context.Users.Remove(user);
            await this.context.SaveChangesAsync();
        }
    }
}
