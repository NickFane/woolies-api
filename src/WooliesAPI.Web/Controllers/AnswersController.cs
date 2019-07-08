using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WooliesAPI.Core.Users.Queries.GetUser;
using WooliesAPI.Domain.Entities;

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
    }
}