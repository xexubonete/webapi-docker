﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    public class GetProductByNameQuery : IRequest<Product>
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Product>
        {
            private readonly IApiDbContext _context;

            public GetProductByNameQueryHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>Handles a request</summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Response from the request</returns>
            public async Task<Product> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Name == request.Name);

                if (product == null)
                {
                    throw new Exception("No names founded" + nameof(Product));
                }

                return product;
            }
        }
    }
}
