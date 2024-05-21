using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection.Interfaces;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Extensions.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using Domain.Extensions.Specification;

namespace Persistence.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository, IScoped
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext= dbContext;
    }

    public async Task<bool> IsClientEnabled(string clientId)
    {
        return true;
    }

    public async Task<bool> AppKeyTakenAsync(string clientId,CancellationToken cancellationToken)=>
        await _dbContext.Clients.AnyAsync(x => x.AppKey == clientId,cancellationToken);


    public async Task<PaginationResponse<TDto>> Search<TDto>(PaginationFilter request,
        CancellationToken cancellationToken) where TDto : class => await this.PaginatedListAsync(new GetAllClientRequestsSpec<TDto>(request),
        request.PageNumber,
        request.PageSize, cancellationToken);

   
    public async Task<List<Client>> GetClientRequest(PaginationFilter request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Clients.ToListAsync();
        return result;
    }

}

public class GetAllClientRequestsSpec<TDto> : EntitiesByPaginationFilterSpec<Client, TDto>
{
    public GetAllClientRequestsSpec(PaginationFilter request)
        : base(request)
    {
    }
}