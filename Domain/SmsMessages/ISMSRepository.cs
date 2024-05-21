using Domain.Dto;
using Domain.Entities;
using Domain.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SmsMessages;

namespace Domain.Abstractions
{
    public interface ISMSRepository : IRepository<SMS>
    {
        Task<List<SMS>> GetSMSRequest(PaginationFilter request, CancellationToken cancellationToken);
        Task<PaginationResponse<SearchSmsQueryResponse>> GetSMSRequests(PaginationFilter request,
        CancellationToken cancellationToken);
        Task<SearchSmsQueryResponse> GetSMSByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
