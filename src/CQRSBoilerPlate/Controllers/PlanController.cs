using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using AspNet.Security.OAuth.Validation;
using CQRSBoilerPlate.Domain.Query;
using CQRSBoilerPlate.Entities.Models;

namespace CQRSBoilerPlate.Controllers
{
    [Produces("application/json")]
    public class PlanController : Controller
    {
        private readonly IMediator _mediator;

        public PlanController(IMediator mediator)
        { 
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/planlist")]
        public async Task<IActionResult> PlanList()
        {
            List<PlanModel> model = await _mediator.SendAsync(new PlanGetAllQuery {});
            if (model == null)
                return NotFound();
            return this.Ok(model);
        }
    }
}