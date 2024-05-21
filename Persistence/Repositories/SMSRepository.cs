using Ardalis.Specification.EntityFrameworkCore;
using Common.DependencyInjection.Interfaces;
using Domain.Abstractions;
using Domain.Dto;
using Domain.Entities;
using Domain.Extensions.Models;
using Domain.Extensions.Specification;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.SmsMessages;

namespace Persistence.Repositories
{
    public class SMSRepository : BaseRepository<SMS>, ISMSRepository, IScoped
    {
        private readonly AppDbContext _dbContext;

        public SMSRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginationResponse<SearchSmsQueryResponse>> GetSMSRequests(PaginationFilter request,
        CancellationToken cancellationToken) => await this.PaginatedListAsync(new GetAllSMSRequestsSpec(request),
        request.PageNumber,
        request.PageSize, cancellationToken);

        public async Task<SearchSmsQueryResponse> GetSMSByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var smsdetails = await _dbContext.SMS
                .FirstOrDefaultAsync(e => e.Id == id.ToString(), cancellationToken: cancellationToken);
            return smsdetails.Adapt<SearchSmsQueryResponse>();
        }

        public async Task<List<SMS>> GetSMSRequest(PaginationFilter request,CancellationToken cancellationToken)
        {
            var result = await _dbContext.SMS.ToListAsync<SMS>();
            return result;
        }
    }


    public class GetAllSMSRequestsSpec : EntitiesByPaginationFilterSpec<SMS, SearchSmsQueryResponse>
    {
        public GetAllSMSRequestsSpec(PaginationFilter request)
            : base(request)
        {
        }
    }
}
