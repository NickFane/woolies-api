using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Persistence.Db;

namespace WooliesAPI.Core.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IDbContext _dbContext;

        public GetUserQueryHandler(IDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.GetUserAsync();
        }
    }
}
