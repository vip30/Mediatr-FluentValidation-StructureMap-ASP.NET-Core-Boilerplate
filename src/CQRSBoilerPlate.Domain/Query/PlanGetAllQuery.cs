using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSBoilerPlate.Entities.Models;
namespace CQRSBoilerPlate.Domain.Query
{
    public class PlanGetAllQuery : IAsyncRequest<List<PlanModel>>
    {
    }
}
