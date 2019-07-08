using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Core.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // TODO
            // Implement DB context and retrieve from there
            return new User()
            {
                Name = "Nick Fane",
                Token = "6e424f40-80a9-49b8-8d66-5921a6734555"
            };
        }
    }
}
