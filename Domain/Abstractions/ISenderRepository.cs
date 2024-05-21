using Domain.Dto;
using Domain.Entities;
using Domain.Extensions.Models;

namespace Domain.Abstractions;

public interface ISenderRepository : IRepository<Sender>
{
    Task<PaginationResponse<SenderDto>> GetSendersRequests(PaginationFilter request, CancellationToken cancellationToken);
}