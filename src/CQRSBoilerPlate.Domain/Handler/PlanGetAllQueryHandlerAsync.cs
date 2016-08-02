using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSBoilerPlate.Domain.Query;
using CQRSBoilerPlate.Entities.Context;
using CQRSBoilerPlate.Entities.Models;
using FluentValidation;
using System.Diagnostics;

namespace CQRSBoilerPlate.Domain.Handler
{
    public class PlanGetAllQueryValidator : AbstractValidator<PlanGetAllQuery>
    {
    }
    public class PlanGetAllQueryHandlerAsync : IAsyncRequestHandler<PlanGetAllQuery, List<PlanModel>>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PlanGetAllQueryHandlerAsync> _logger;
        public PlanGetAllQueryHandlerAsync(IMapper mapper, ApplicationDbContext db, ILogger<PlanGetAllQueryHandlerAsync> logger)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
        }
        public async Task<List<PlanModel>> Handle(PlanGetAllQuery message)
        {
            return await _db.Plans.Include(c=>c.PlanFeatures).ThenInclude(d=>d.Feature).ProjectTo<PlanModel>().ToListAsync();
        }
    }
}
