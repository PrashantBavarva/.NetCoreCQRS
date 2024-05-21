// using Common.DependencyInjection.Interfaces;
// using Domain.Abstractions;
// using Domain.Dto;
// using Domain.Entities;
// using Domain.Extensions.Models;
// using Domain.Extensions.Specification;
// using Persistence.Repositories.Base;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace Persistence.Repositories
// {
//     public class RecipientRepository : BaseRepository<Recipient>, IRecipientsRepository, IScoped
//     {
//         private readonly AppDbContext _dbContext;
//
//
//         public async Task<PaginationResponse<RecipientsDto>> GetRecupientRequests(PaginationFilter request,
//         CancellationToken cancellationToken) => await this.PaginatedListAsync(new GetAllRecipientRequestsSpec(request),
//         request.PageNumber,
//         request.PageSize, cancellationToken);
//
//         public RecipientRepository(AppDbContext dbContext) : base(dbContext)
//         {
//             _dbContext = dbContext;
//         }
//
//
//     }
//
//
//     public class GetAllRecipientRequestsSpec : EntitiesByPaginationFilterSpec<Recipient, RecipientsDto>
//     {
//         public GetAllRecipientRequestsSpec(PaginationFilter request)
//             : base(request)
//         {
//         }
//     }
// }
