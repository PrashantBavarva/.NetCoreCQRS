using Common.DependencyInjection.Interfaces;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SettingRepository : ISettingRepository, IScoped
    {
        public async Task<bool> ApiKeyEnabled()
        {
            return true;
        }
    }
}
