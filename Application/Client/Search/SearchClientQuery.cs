using Domain.Dto;
using Domain.Extensions.Models;
using LanguageExt.Common;


namespace Application.Client.Search
{
    public class SearchClientQuery : PaginationFilter,IQuery<PaginationResponse<SearchClientsResponse>>
    {
    }
    public class SearchClientQueryHandler : IQueryHandler<SearchClientQuery, PaginationResponse<SearchClientsResponse>>
    {
        private readonly IClientRepository _clientRepository;

        public SearchClientQueryHandler(IClientRepository clientRepository) { _clientRepository = clientRepository;  }

        public async Task<Result<PaginationResponse<SearchClientsResponse>>> Handle(SearchClientQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.Search<SearchClientsResponse>(request, cancellationToken);
            return result;
        }
    }

    public class SearchClientsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AppKey { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
