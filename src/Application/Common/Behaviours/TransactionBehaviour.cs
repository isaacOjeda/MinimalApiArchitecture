using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Common.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

        public TransactionBehaviour(ApiDbContext context, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();
                var response = await next();
                await _context.CommitTransactionAsync();

                return response;
            }
            catch (Exception)
            {
                _logger.LogError("Request failed: Rolling back all the changes made to the Context");

                await _context.RollbackTransaction();
                throw;
            }
        }
    }
}
