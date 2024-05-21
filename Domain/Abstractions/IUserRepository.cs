using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<List<Users>> GetUsersAsync(CancellationToken cancellationToken);
        Task<Users> GetUserByIdAsync(CancellationToken cancellationToken,string id);
    }
}
