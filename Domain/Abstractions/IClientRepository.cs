using Domain.Dto;
using Domain.Entities;
using Domain.Extensions.Models;

namespace Domain.Abstractions;

public interface IClientRepository : IRepository<Client>
{
    Task<PaginationResponse<TDto>> Search<TDto>(PaginationFilter request, CancellationToken cancellationToken)where TDto : class;
    Task<bool> IsClientEnabled(string clientId);
    Task<bool> AppKeyTakenAsync(string clientId,CancellationToken cancellationToken=default);
}