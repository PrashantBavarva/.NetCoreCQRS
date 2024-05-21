using Common.DependencyInjection.Interfaces;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Extensions.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository, IScoped
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Users> GetUserByIdAsync(CancellationToken cancellationToken, string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
            return user;
        }

        public async Task<List<Users>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.ToListAsync(cancellationToken);
            throw new NotImplementedException();
        }
    }
}
