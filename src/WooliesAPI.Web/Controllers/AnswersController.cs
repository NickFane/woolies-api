using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WooliesAPI.Core.ShopperHistories.Queries;
using WooliesAPI.Core.Trollies.Queries.GetTrolleyTotal;
using WooliesAPI.Core.Users.Queries.GetUser;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Domain.Enums;

namespace WooliesAPI.Web.Controllers
{
    [ApiController]
    public class AnswersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<User>> User()
        {
            var result = await Mediator.Send(new GetUserQuery());

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Sort(SortOption sortOption)
        {
            var result = await Mediator.Send(new GetProductsQuery(sortOption));

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<decimal>> TrolleyTotal([FromBody] GetTrolleyTotalQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }
    }
}