using Common.DependencyInjection.Interfaces;
using Domain.Abstractions;
using Domain.Dto;
using Domain.Entities;
using Domain.Extensions.Models;
using Persistence.Repositories.Base;
using System.Threading.Tasks;
using System.Threading;
using Domain.Extensions.Specification;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.Repositories;

public class SenderRepository : BaseRepository<Sender>, ISenderRepository, IScoped
{
    private readonly AppDbContext _dbContext;
    public SenderRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResponse<SenderDto>> GetSendersRequests(PaginationFilter request,
        CancellationToken cancellationToken) => await this.PaginatedListAsync(new GetAllSenderRequestsSpec(request),
        request.PageNumber,
        request.PageSize, cancellationToken);

}


public class GetAllSenderRequestsSpec : EntitiesByPaginationFilterSpec<Sender, SenderDto>
{
    public GetAllSenderRequestsSpec(PaginationFilter request)
        : base(request)
    {
    }
}