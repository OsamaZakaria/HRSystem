using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Data;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Core.Behaviour
{
    internal sealed class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
    {
        private readonly ApplicationDbContext _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TransactionBehaviour(ApplicationDbContext unitOfWork) => _unitOfWork = unitOfWork;

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IQuery<TResponse>)
            {
                return await next();
            }

            await using IDbContextTransaction transaction = await _unitOfWork.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                TResponse response = await next();

                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}
