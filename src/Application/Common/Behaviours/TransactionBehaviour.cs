using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Common.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse>(
        ApiDbContext context,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                await context.BeginTransactionAsync();
                var response = await next();
                await context.CommitTransactionAsync();

                return response;
            }
            catch (Exception)
            {
                logger.LogError("Request failed: Rolling back all the changes made to the Context");

                await context.RollbackTransaction();
                throw;
            }
        }
    }
}
