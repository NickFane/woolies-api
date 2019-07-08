using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Core.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
    }
}
