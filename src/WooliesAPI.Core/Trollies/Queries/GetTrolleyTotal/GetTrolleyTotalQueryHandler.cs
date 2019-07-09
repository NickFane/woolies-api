using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WooliesAPI.Persistence.Api;

namespace WooliesAPI.Core.Trollies.Queries.GetTrolleyTotal
{
    public class GetTrolleyTotalQueryHandler : IRequestHandler<GetTrolleyTotalQuery, decimal>
    {
        private readonly IResourceApi _resourceApi;

        public GetTrolleyTotalQueryHandler(IResourceApi resourceApi)
        {
            _resourceApi = resourceApi;
        }

        public async Task<decimal> Handle(GetTrolleyTotalQuery request, CancellationToken cancellationToken)
        {
            // Even though both query models are the same
            // We'll break SoC if we allow our persistence layer to understand queries from our Core/Application
            var trolleyCalculatorQuery = new Domain.Entities.TrolleyCalculatorQuery()
            { Products = request.Products, Quantities = request.Quantities, Specials = request.Specials };

            var trolleyTotal = await _resourceApi.GetTrolleyTotalAsync(trolleyCalculatorQuery);
            return trolleyTotal;
        }
    }
}
